using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PICO8Tool
{
	partial class MainForm : Form
	{
		readonly Color[] picoPalette = new Color[]
		{
			Color.FromArgb(0, 0, 0),		// 0 black 
			Color.FromArgb(29, 43, 83),		// 1 dark blue 
			Color.FromArgb(126, 37, 83),	// 2 dark purple 
			Color.FromArgb(0, 135, 81),		// 3 dark green 
			Color.FromArgb(171, 82, 54),	// 4 brown 
			Color.FromArgb(95, 87, 79),		// 5 dark gray 
			Color.FromArgb(194, 195, 199),	// 6 light gray 
			Color.FromArgb(255, 241, 232),	// 7 white
			Color.FromArgb(255, 0, 77),		// 8 red 
			Color.FromArgb(255, 163, 0),	// 9 orange 
			Color.FromArgb(255, 255, 39),	// a yellow 
			Color.FromArgb(0, 231, 88),		// b light green 
			Color.FromArgb(41, 173, 255),	// c light blue 
			Color.FromArgb(131, 118, 156),	// d light purple 
			Color.FromArgb(255, 119, 168),	// e dark pink 
			Color.FromArgb(255, 204, 170)	// f light pink 
		};

		private string p8file;
		private string imgFile;

		private SaveFileDialog saveDialog;

		public MainForm()
		{
			InitializeComponent();
			picViewer.CurrentScaleChanged += PicViewer_CurrentScaleChanged;
			saveDialog = new SaveFileDialog();
        }

		private string P8File
		{
			get { return p8file ?? ""; }
			set
			{
				p8file = value;
				var title = Application.ProductName;
				if (!String.IsNullOrWhiteSpace(p8file))
				{
					Text = Path.GetFileName(p8file) + " - " + Application.ProductName;
				}
				else
				{
					Text = Application.ProductName;
                }
            }
		}

		private void PicViewer_CurrentScaleChanged(object sender, EventArgs e)
		{
			lblZoom.Text = String.Format("{0}%", (int)(picViewer.CurrentScale * 100));
		}

		private void btnShowSettings_Click(object sender, EventArgs e)
		{
			splitContainer.Panel2Collapsed = !splitContainer.Panel2Collapsed;
		}

		protected override void OnDragEnter(DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			var files = (string[])(e.Data.GetData(DataFormats.FileDrop, false));
			if (files.Length > 0) LoadFile(files[0]);
		}

		private void LoadFile(string file)
		{
			if (String.Compare(Path.GetExtension(file), ".p8", true) == 0)
			{
				ExtractP8(file);
			}
			else
            if (SupportedImageType(file))
			{
				LoadImage(file);
            }
			else
			{
				Activate();
				MessageBox.Show(this, "Unsupported file type", "Load Image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private string EncodeImage(Bitmap img)
		{
			var ary = new byte[128*128];
			var bmp = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
			unsafe
			{
				Marshal.Copy(bmp.Scan0, ary, 0, 128 * 128);
			}
			img.UnlockBits(bmp);

			var sb = new System.Text.StringBuilder(128 * 129);
			int incy = 0;
			for (int y = 0; y < 128; y++)
			{
				for (int x = 0; x < 128; x++)
				{
					var s = ary[incy + x].ToString("x");
					sb.Append(s);
				}
				sb.AppendLine(); // dont append last blank line
				incy += 128;
			}

			return sb.ToString();
		}

		private void EmbedP8(string file, Image img)
		{
			try
			{
				// open .p8
				var p8 = File.ReadAllText(file);
				var sb = new System.Text.StringBuilder(p8.Length);
				using (var s = new StringReader(p8))
				{
					string line;
					do
					{
						line = s.ReadLine();
						if (line != null)
						{
							sb.AppendLine(line);
							if (line.StartsWith("__gfx__"))
							{
								// append new graphics
								sb.Append(EncodeImage((Bitmap)img));
								
								// skip old __gfx__ lines
								do
								{
									line = s.ReadLine();
									if (line != null && line.StartsWith("__"))
									{
										// append next section
										sb.AppendLine(line);
										break;
									}
								} while (line != null);
							}
						}
					} while (line != null);
				}

				// save it
				File.WriteAllText(file, sb.ToString());

				MessageBox.Show(this, "Cart updated: " + Path.GetFileName(file), "Embed Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch(Exception ex)
			{
				Activate();
				MessageBox.Show(this, "Error embeding cart: " + ex.Message, "Embed Cart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void FindGFXBegin(TextReader s)
		{
			bool found = false;
			while (!found)
			{
				var line = s.ReadLine();
				if (line == null) break;

				if (line.StartsWith("__gfx__"))
				{
					found = true;
				}
			}
			if (!found) throw new Exception("Unable to find __gfx__ entry");
		}

		private void FindGFXEnd(TextReader s, Action<char> handler)
		{
			bool found = false;
			while (!found)
			{
				var line = s.ReadLine();
				if (line == null) break;

				if (line.StartsWith("__"))
				{
					found = true;
				}
				else
				{
					// convert hex to byte
					foreach (var ch in line)
					{
						handler(ch);
					}
				}
			}

			if (!found) throw new Exception("Unable to find end of __gfx__ entry");
		}

		private void ExtractP8(string file)
		{
			try
			{
				var pixels = new List<byte>();

				// open .p8
				using (var s = File.OpenText(file))
				{
					// search for graphics line
					FindGFXBegin(s);

					// find endpoint and extract pixels
					FindGFXEnd(s, ch =>
					{
						// convert hex to byte
						try
						{
							var idx = (byte)Convert.ToInt32(ch.ToString(), 16);
							pixels.Add(idx);
						}
						catch (Exception ex)
						{
							throw new Exception("Bad conversion", ex);
						}
					});
				}

				// generate 128x128 image
				var img = new Bitmap(128, 128, PixelFormat.Format8bppIndexed);

				// replace with PICO-8 palette
				var pal = img.Palette;
				for (int i = 0; i < 256; i++)
				{
					pal.Entries[i] = i < 16 ? picoPalette[i] : Color.Black;
                }

				img.Palette = pal;

				// copy bits
				var ary = pixels.ToArray();
				var bmp = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
				unsafe
				{
					Marshal.Copy(ary, 0, bmp.Scan0, 128 * 128);
				}
				img.UnlockBits(bmp);

				// update
				SetImage(img);
				SetP8File(file);
		    }
			catch (Exception ex)
			{
				Activate();
				MessageBox.Show(this, "Error loading cart: " + ex.Message, "Load Cart", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void SetP8File(string file)
		{
			P8File = file;

			if (String.IsNullOrWhiteSpace(file))
			{
				btnEmbed.Enabled = false;
				imgFile = string.Empty;
			}
			else
			{
				btnEmbed.Enabled = true;
				imgFile = Path.ChangeExtension(file, ".png");
			}
		}

		private void SetImage(Image img)
		{
			picViewer.Image = img;
			btnExtract.Enabled = img != null;
		}

		private void LoadImage(string file)
		{
			try
			{
				var img = new Bitmap(file);

				// validate size and palette
				if (img.Width != 128 || img.Height != 128) throw new Exception("Image size must be 128x128");
				if (img.PixelFormat != PixelFormat.Format8bppIndexed) throw new Exception("Only 8bit palette images supported");
				if (img.Palette.Entries.Length < 16) throw new Exception("Invalid palette size");
				for (int i=0; i< 16; i++)
				{
					if (img.Palette.Entries[i] != picoPalette[i]) throw new Exception("Palette does not match PICO-8");
				}

				SetImage(img);
				imgFile = file;
			}
			catch (Exception ex)
			{
				Activate();
				MessageBox.Show(this, "Error loading image: " + ex.Message, "Load Image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void btnToggleGrid_Click(object sender, EventArgs e)
		{
			picViewer.DarkGrid = !picViewer.DarkGrid;
        }

		private void btnResetView_Click(object sender, EventArgs e)
		{
			picViewer.CurrentScale = 1.0f;
			picViewer.WorkspaceOrigin = PointF.Empty;
		}

		private void btnExtract_Click(object sender, EventArgs e)
		{
			if (picViewer.Image != null)
			{
				saveDialog.Filter = "PNG Files|*.png|All Files|*.*";
				saveDialog.DefaultExt = ".png";
				saveDialog.Title = "Save PNG";
				saveDialog.InitialDirectory = String.IsNullOrWhiteSpace(imgFile) ? "" : Path.GetDirectoryName(imgFile);
				saveDialog.FileName = Path.GetFileName(imgFile);

				if (saveDialog.ShowDialog(this) == DialogResult.Cancel)
				{
					return;
				}

				try
				{
					SaveImage(picViewer.Image, saveDialog.FileName);
					imgFile = saveDialog.FileName;
				}
				catch (Exception ex)
				{
					Activate();
					MessageBox.Show(this, "Error saving image: " + ex.Message, "Save Image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
        }

		private bool SupportedImageType(string file)
		{
			var ext = Path.GetExtension(file).ToLower();

			if (!String.IsNullOrWhiteSpace(ext))
			{
				foreach (var ici in ImageCodecInfo.GetImageDecoders())
				{
					if (ici.FilenameExtension.ToLower().Contains(ext)) return true;
				}
			}

			return false;
		}

		private void SaveImage(Image img, string file)
		{
			ImageCodecInfo iciPng = null;
			foreach (var ici in ImageCodecInfo.GetImageDecoders())
			{
				if (ici.FilenameExtension.ToLower().Contains("png"))
				{
					iciPng = ici;
					break;
				}
			}

			if (iciPng == null) throw new Exception("PNG encoder not available");

			var eps = new EncoderParameters(1);
			var ep = new EncoderParameter(Encoder.ColorDepth, 8L);
			eps.Param[0] = ep;
			img.Save(file, iciPng, eps);
		}

		private void btnEmbed_Click(object sender, EventArgs e)
		{
			EmbedP8(P8File, picViewer.Image);
        }

		private void btnNew_Click(object sender, EventArgs e)
		{
			SetImage(null);
			SetP8File(null);
		}

		private void btnAbout_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this, "PICO8Tool v" + Application.ProductVersion + "\nby Scott Ramsay\nhttps://github.com/foobit/PICO8Tool", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
