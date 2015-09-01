using System;
using System.Drawing;

namespace PICO8Tool
{
	static class GraphicsExtensions
	{
		public static void DrawTiled(this Graphics g, int x, int y, int width, int height, Image image)
		{
			var r = new Rectangle(x, y, width, height);
			using (var b = new TextureBrush(image))
			{
				g.FillRectangle(b, r);
			}
		}

		public static void DrawImageCentered(this Graphics g, Image img, int x, int y)
		{
			int w = img.Width;
			int h = img.Height;
			x -= w / 2;
			y -= h / 2;
			g.DrawImage(img, x, y, w, h);
		}
	}
}
