using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ph2beta
{
	public partial class Form1 : Form
	{
		protected Bitmap img1 = null; // окошко изменения
		List<Point> point = new List<Point> { new Point(0, 512), new Point(512, 0) };
		public Form1()
		{
			InitializeComponent();
			img1 = (Bitmap)pictureBox1.Image;
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			point.Add(new Point(e.X, e.Y));
			point.Sort((x, y) => x.X.CompareTo(y.X));

			using (Graphics g = Graphics.FromImage(img1))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.Clear(Color.White);
			}

			pictureBox1.Refresh();
			drawing();
			//координаты в окошки
			textBox1.Text = Convert.ToString(e.X);
			textBox2.Text = Convert.ToString(e.Y);

			//точка для красоты
			Graphics gg = pictureBox1.CreateGraphics();
			Pen pen = new Pen(Color.Black, 8f);
			gg.DrawEllipse(pen, e.X - 3, e.Y - 3, 8, 8);
		}

		private void drawing()
		{
			using (Graphics g = Graphics.FromImage(img1))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.SmoothingMode = SmoothingMode.HighQuality;
				var p = Pens.Red.Clone() as Pen;
				p.Width = 5;
				for (int i = 0; i < point.Count - 1; i++)
				{
					PointF point1 = new PointF(point[i].X, point[i].Y);
					PointF point2 = new PointF(point[i + 1].X, point[i + 1].Y);
					g.DrawLine(p, point1, point2);
				}
			}
			pictureBox1.Refresh();
		}
	}
}
