using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace PICO8Tool
{
	partial class PicViewer : UserControl
	{
		private enum DragMode
		{
			Idle,
			Workspace
		}

		private const float maxZoom = 8.0f;
		private const float minZoom = 0.25f;
		private const float largeChange = 0.05f;
		private bool showGrid = true;
		private bool darkGrid = true;
		private float scale = 1.0f;
		private PointF origin = PointF.Empty;
		private DragMode dragMode = DragMode.Idle;
		private Point dragPoint;

		private GraphicsState savedState;
		private Image currentImage;

		public event EventHandler CurrentScaleChanged;
		public event EventHandler OriginChanged;

		public PicViewer()
		{
			InitializeComponent();
			DoubleBuffered = true;

			Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
			MinScale = minZoom;
			MaxScale = maxZoom;
			CurrentScale = 1.0f;

			var ck = new Curve();
			ck.Add(0.0f, 1.0f);
			ck.Add(0.75f, 3.0f);
			ck.Add(1.0f, 6.0f);
			ZoomCurve = ck;
		}

		[Browsable(false)]
		public Curve ZoomCurve { get; private set; }

		[Browsable(false)]
		public Font ScaledFont { get; private set; }

		[Browsable(false)]
		public Font ScaledBoldFont { get; private set; }

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float MinScale { get; set; }

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public float MaxScale { get; set; }

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowGrid
		{
			get { return showGrid; }
			set
			{
				showGrid = value;
				Invalidate();
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PointF WorkspaceOrigin
		{
			get { return origin; }
			set
			{
				origin = value;
				OnOriginChanged(EventArgs.Empty);
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool DarkGrid
		{
			get { return darkGrid; }
			set
			{
				darkGrid = value;
				Invalidate();
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Image Image
		{
			get { return currentImage; }
			set
			{
				if (currentImage != null) currentImage.Dispose();
				currentImage = value;
				Invalidate();
            }
		}

		[Browsable(false)]
		public PointF Center
		{
			get
			{
				return new PointF(
					ClientSize.Width * 0.5f,
					ClientSize.Height * 0.5f);
			}
		}

		[Category("Appearance")]
		[DefaultValue(1.0f)]
		public float CurrentScale
		{
			get { return scale; }
			set
			{
				scale = Math.Min(Math.Max(minZoom, value), maxZoom);
				if (ScaledFont != null)
				{
					ScaledFont.Dispose();
				}

				if (ScaledBoldFont != null)
				{
					ScaledBoldFont.Dispose();
				}

				ScaledFont = new Font(Font.Name, Font.Size * scale);
				ScaledBoldFont = new Font(ScaledFont, FontStyle.Bold);

				Invalidate();
				OnCurrentScaleChanged(EventArgs.Empty);
			}
		}

		public void BeginTranslate(Graphics g)
		{
			savedState = g.Save();

			var point = Center;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			g.PageUnit = GraphicsUnit.Pixel;
			g.TranslateTransform(-WorkspaceOrigin.X + point.X / CurrentScale, -WorkspaceOrigin.Y + point.Y / CurrentScale);
			g.PageScale = CurrentScale;
		}

		public void EndTranslate(Graphics g)
		{
			g.Restore(savedState);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			Invalidate();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			if (showGrid)
			{
				var img = darkGrid ? Properties.Resources.checker0 : Properties.Resources.checker1;
                e.Graphics.DrawTiled(0, 0, Width, Height, img);
			}
			else
			{
				base.OnPaintBackground(e);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			PaintWorkspace(e.Graphics);
		}

		protected virtual void PaintWorkspace(Graphics g)
		{
			if (Image != null)
			{
				BeginTranslate(g);
				g.InterpolationMode = InterpolationMode.NearestNeighbor;
				g.DrawImageCentered(Image, 0, 0);
				EndTranslate(g);
			}
			else
			{
				var sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;
				var br = darkGrid ? Brushes.White : Brushes.Black;
				g.DrawString("Drag and Drop PNG image or P8 cart", Font, br, ClientRectangle, sf);
			}
		}

		protected virtual void OnOriginChanged(EventArgs e)
		{
			var eh = OriginChanged;
			if (eh != null)
			{
				eh(this, e);
			}
		}

		protected virtual void OnCurrentScaleChanged(EventArgs e)
		{
			var eh = CurrentScaleChanged;
			if (eh != null)
			{
				eh(this, e);
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			float s = (CurrentScale - MinScale) / (MaxScale - MinScale);
			CurrentScale += ZoomCurve.Eval(s) * Math.Sign(e.Delta) * largeChange;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			Focus();

			if (dragMode == DragMode.Idle)
			{
				if (e.Button == MouseButtons.Left)
				{
					dragPoint.X = e.X;
					dragPoint.Y = e.Y;

					dragMode = DragMode.Workspace;
					Capture = true;
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			switch (dragMode)
			{
				case DragMode.Workspace:
					{
						var pt = new PointF(dragPoint.X - e.X, dragPoint.Y - e.Y);
						pt.X /= CurrentScale;
						pt.Y /= CurrentScale;
						WorkspaceOrigin = new PointF(WorkspaceOrigin.X + pt.X, WorkspaceOrigin.Y + pt.Y);
						dragPoint = new Point(e.X, e.Y);
						Invalidate();
					}
					break;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (dragMode == DragMode.Workspace && e.Button == MouseButtons.Left)
			{
				Cursor = Cursors.Default;
				dragMode = DragMode.Idle;
				Capture = false;
				Invalidate();
			}
		}
	}
}
