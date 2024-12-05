using ControlLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRPR
{
    public partial class FReport : Form
    {
        CSinhVienDao svdao = new CSinhVienDao();
        string ms;
        string madetai;
        public FReport(string ms, string madetai)
        {
            InitializeComponent();
            this.ms = ms;
            this.madetai = madetai;
        }

        private void FReport_Load(object sender, EventArgs e)
        {
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.DataSource = svdao.NhiemVuDuocGiao(ms);
            dataGridView1.Columns["Column1"].DataPropertyName = "manv";
            dataGridView1.Columns["CTieuDe"].DataPropertyName = "tieude";
            dataGridView1.Columns["CNguoiGui"].DataPropertyName = "hoten";
            dataGridView1.Columns["CThoiHan"].DataPropertyName = "ngayhethan";
            dataGridView1.Columns["CTrangThai"].DataPropertyName = "trangthai";
        }

        private void FReport_Load2()
        {
            dataGridView2.CellPainting += dataGridView1_CellPainting;
            dataGridView2.DataSource = svdao.NhiemVuDaGiao(ms);
            dataGridView2.Columns["manv"].Visible = false;
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

                dataGridView1.ClearSelection();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string value1 = row.Cells["manv"].Value.ToString();
                DataTable dt = svdao.NhiemVuChiTiet(value1); 
                string noidung = "";
                if (dt.Rows.Count > 0)
                {
                    noidung = dt.Rows[0]["noidung"].ToString();
                }

                FDetailsReport fDetailsReport = new FDetailsReport(value1, madetai, noidung);
                fDetailsReport.Show();
            }

        }

        private void fAddTask_FormClosed(object sender, FormClosedEventArgs e)
        {
            FReport_Load(sender, e);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            FAddTask fAddTask = new FAddTask(ms, madetai);
            fAddTask.FormClosed += fAddTask_FormClosed;
            fAddTask.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1) // Kiểm tra xem tab được chọn là tabPage2
            {
                FReport_Load(sender, e);
            }
            else if (tabControl1.SelectedTab == tabPage2) // Kiểm tra xem tab được chọn là tabPage2
            {
                FReport_Load2();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                // Mở hộp thoại chọn file
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Chọn file cần upload",
                    Filter = "All files (*.*)|*.*"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Đọc dữ liệu file
                    byte[] fileData = File.ReadAllBytes(openFileDialog.FileName); // Đọc dữ liệu file thành mảng byte[]

                    // Tự động cấu hình tên file
                    string fileExtension = Path.GetExtension(openFileDialog.FileName); // Lấy phần mở rộng (ví dụ: .pdf, .docx)
                    string fileName = $"DeTai_{madetai}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}"; // Đặt tên file (ví dụ: DeTai_1_20231205_153000.pdf)

                    // Gọi hàm lưu vào cơ sở dữ liệu
                    Task task = svdao.SaveFileToDatabase(fileName, fileData, madetai);

                    // Hiển thị thông báo thành công
                    MessageBox.Show("File đã được lưu thành công vào cơ sở dữ liệu!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tieude = textBox1.Text;
            string noidung = textBox2.Text;
            DateTime thoigiannop = DateTime.Now;
            string msnguoinop = ms;
            svdao.SaveSanPhamDetailsAsync(madetai, tieude, noidung, thoigiannop, msnguoinop);
        }
    }
}
