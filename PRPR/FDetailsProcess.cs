using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRPR
{
    public partial class FDetailsProcess : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        string ms;
        string madetai;
        public FDetailsProcess(string madetai, string ms)
        {
            InitializeComponent();
            this.madetai = madetai;
            this.ms = ms;
        }

        private void FDetailsProcess_Load(object sender, EventArgs e)
        {
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.DataSource = gvdao.ChiTietTienDo(madetai);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            FAddTask fAddTask = new FAddTask(ms, madetai);
            fAddTask.ShowDialog();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Kiểm tra chỉ số cột và chỉ số dòng để đảm bảo chỉ tô màu và định dạng header cột
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                // Tùy chỉnh màu nền cho header cột
                using (SolidBrush brush = new SolidBrush(Color.HotPink))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                // Tùy chỉnh phông chữ cho header cột
                using (Font font = new Font("Arial", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Value.ToString(), font, Brushes.White, e.CellBounds, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }

                // Vẽ viền cho header cột
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(pen, rect);
                }

                // Ngăn không cho DataGridView vẽ nền và nội dung mặc định
                e.Handled = true;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Font fontrow = new Font("Times New Roman", 9, FontStyle.Bold);
                    row.DefaultCellStyle.Font = fontrow;
                }

                // Vô hiệu hóa dòng trống cuối cùng trong DataGridView
                dataGridView1.AllowUserToAddRows = false;
            }
        }
    }
}
