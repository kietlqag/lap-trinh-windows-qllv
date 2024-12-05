using ControlLibrary;
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
    public partial class FProjetGV : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        string ms;
        public FProjetGV(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void FProjetGV_Load(object sender, EventArgs e)
        {
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.DataSource = gvdao.Load_DTC();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Kiểm tra nếu là tiêu đề cột
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                // Vẽ nền màu hồng nóng
                e.PaintBackground(e.CellBounds, true);
                using (SolidBrush brush = new SolidBrush(Color.HotPink))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                // Vẽ nội dung tiêu đề
                using (Font font = new Font("Times New Roman", 10, FontStyle.Bold))
                {
                    var format = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    TextRenderer.DrawText(
                        e.Graphics,
                        e.Value?.ToString(),
                        font,
                        e.CellBounds,
                        Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak
                    );
                }

                // Vẽ viền ô
                using (Pen pen = new Pen(Color.Gray, 1))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(pen, rect);
                }

                e.Handled = true;
            }

            // Tắt tính năng thêm hàng mới
            dataGridView1.AllowUserToAddRows = false;
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2) 
            {
                load();
            }
        }

        private void load()
        {
            dataGridView2.CellPainting += dataGridView1_CellPainting;
            dataGridView2.DataSource = gvdao.Load_DTR(ms);
        }
        private void label11_Click(object sender, EventArgs e)
        {
            FAddProeject fAddProeject = new FAddProeject(ms);
            fAddProeject.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            string filterColumn = comboBox2.Text;
            string filterValue = textBox2.Text;
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.DataSource = gvdao.Load_DTTK(filterColumn, filterValue);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FProjetGV_Load(sender, e);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[selectedRow];
            object masv1Value = row.Cells["Mã sinh viên 1"].Value;
            object masv2Value = row.Cells["Mã sinh viên 2"].Value;
            if (masv1Value != null && masv2Value != null && !string.IsNullOrEmpty(masv1Value.ToString()) && !string.IsNullOrEmpty(masv2Value.ToString()))
            {
                DialogResult result = MessageBox.Show("Bạn có muốn duyệt đề tài này?", "Xác nhận", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    CapNhatTrangThai(row.Cells["madetai"].Value.ToString());
                    load();
                }
            }
            else
            {
                MessageBox.Show("Chưa có sinh viên đăng ký.");
            }
        }

        private void CapNhatTrangThai(string madetai)
        {
            gvdao.CapNhatTT(madetai);
        }
    }
}
