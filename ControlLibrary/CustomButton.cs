using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class CustomButton : Button
    {
        private int cornerRadius = 20; // Bán kính góc bo
        private Color[] cornerColors = new Color[4]; // Mảng màu cho các góc

        public CustomButton()
        {
            Width = 100;
            Height = 50;
            Text = "Click Me";

            // Gắn sự kiện Click, MouseEnter và MouseLeave cho nút
            Click += CustomButton_Click;
            MouseEnter += CustomButton_Enter;
            MouseLeave += CustomButton_Leave;

            // Khởi tạo màu cho các góc
            for (int i = 0; i < cornerColors.Length; i++)
            {
                cornerColors[i] = Color.HotPink; // Màu mặc định là đỏ
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            // Vẽ hình chữ nhật bo góc
            Graphics graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(BackColor);

            RectangleF rect = new RectangleF(0, 0, Width, Height);

            using (GraphicsPath path = GetRoundedRectanglePath(rect, cornerRadius))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.Vertical))
                {
                    ColorBlend colorBlend = new ColorBlend();
                    colorBlend.Colors = cornerColors;
                    colorBlend.Positions = new float[] { 0, 0.33f, 0.67f, 1 };

                    brush.InterpolationColors = colorBlend;
                    graphics.FillPath(brush, path);
                }
            }

            using (Pen pen = new Pen(ForeColor))
            {
                graphics.DrawPath(pen, GetRoundedRectanglePath(rect, cornerRadius));
            }

            using (SolidBrush brush = new SolidBrush(ForeColor))
            {
                SizeF textSize = graphics.MeasureString(Text, Font);
                PointF textLocation = new PointF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
                graphics.DrawString(Text, Font, brush, textLocation);
            }
        }

        private void CustomButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.LavenderBlush;
            cornerColors[0] = Color.Red;
            cornerColors[1] = Color.Red;
            cornerColors[2] = Color.Red;
            cornerColors[3] = Color.Red;
            Invalidate();
        }

        private GraphicsPath GetRoundedRectanglePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            float diameter = radius * 2;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(rect.Location, sizeF);

            // Góc trên bên trái
            path.AddArc(arc, 180, 90);

            // Góc trên bên phải
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Góc dưới bên phải
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Góc dưới bên trái
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            return path;
        }

        private void CustomButton_Leave(object sender, EventArgs e)
        {
            BackColor = Color.LavenderBlush;
            cornerColors[0] = Color.HotPink;
            cornerColors[1] = Color.HotPink;
            cornerColors[2] = Color.HotPink;
            cornerColors[3] = Color.HotPink;
            Invalidate();
        }

        private void CustomButton_Enter(object sender, EventArgs e)
        {
            BackColor = Color.LavenderBlush;
            cornerColors[0] = Color.Yellow;
            cornerColors[1] = Color.Yellow;
            cornerColors[2] = Color.Yellow;
            cornerColors[3] = Color.Yellow;
            Invalidate();
        }
    }
}