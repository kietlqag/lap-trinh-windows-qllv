using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRPR
{
    public partial class FMarkGV : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        string ms;
        public FMarkGV(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "diem")
                {
                    Double newValue;
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                        Double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out newValue))
                    {
                        ((DataTable)dataGridView1.DataSource).Rows[e.RowIndex]["diem"] = newValue;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Mã số sinh viên"].Value != null)
                    {
                        string maSV = row.Cells["Mã số sinh viên"].Value.ToString();
                        if (row.Cells["Điểm"].Value != null)
                        {
                            double diem = Convert.ToDouble(row.Cells["Điểm"].Value);
                            gvdao.CapNhapDiem(diem, maSV);
                        }
                        else
                        {
                            MessageBox.Show("Giá trị của ô Điểm tại hàng " + (row.Index + 1) + " không hợp lệ.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Giá trị của ô Mã số sinh viên tại hàng " + (row.Index + 1) + " không hợp lệ.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Mã số")
            {
                dataGridView1.CellPainting += dataGridView1_CellPainting;
                dataGridView1.DataSource = gvdao.Load_Diem2(textBox1.Text);
            }
            else if (comboBox1.Text == "Họ tên")
            {
                dataGridView1.CellPainting += dataGridView1_CellPainting;
                dataGridView1.DataSource = gvdao.Load_Diem3(textBox1.Text);
            }
        }

        private void FMarkGV_Load_1(object sender, EventArgs e)
        {
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.DataSource = gvdao.Load_Diem(ms);
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

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Resizable = DataGridViewTriState.False;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FMarkGV_Load_1(sender, e);
        }
    }
}
