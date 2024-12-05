using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using ControlLibrary;
using System.IO;

namespace PRPR
{
    public partial class FGiangVien : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        string ms;
        public FGiangVien(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private Form currentFormChild;
        private void OpenchildForm(Form childFrom)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childFrom;
            childFrom.TopLevel = false;
            childFrom.FormBorderStyle = FormBorderStyle.None;
            childFrom.Dock = DockStyle.Fill;
            panel11.Controls.Add(childFrom);
            panel11.Tag = childFrom;
            childFrom.BringToFront();
            childFrom.Show();
        }

        private void FGiangVien_Load(object sender, EventArgs e)
        {
            string hoten = gvdao.GetHoTenByMaSo(ms);
            label2.Text = ms + "\n" + hoten;

            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.DataSource = gvdao.Load(ms);
            dataGridView1.Columns["matb"].DataPropertyName = "matb";
            dataGridView1.Columns["Column1"].DataPropertyName = "tieude";
            dataGridView1.Columns["Column2"].DataPropertyName = "hoten";
            dataGridView1.Columns["Column3"].DataPropertyName = "thoidiem";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string fileName = "Email-icon.png";
                string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                if (!string.IsNullOrEmpty(imagePath))
                {
                    Image image = Image.FromFile(imagePath);
                    row.Cells["hinhanh"].Value = image;
                }
            }
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

        private void label3_Click(object sender, EventArgs e)
        {
            string fileName = "Home-icon.png";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Image image = Image.FromFile(imagePath);
            pictureBox10.Image = image;
            label10.Text = "TRANG CHỦ";
            if (currentFormChild != null)
            {
                currentFormChild.Close();
                FGiangVien_Load(sender as Form, e);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            string fileName = "user-icon.png";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Image image = Image.FromFile(imagePath);
            label10.Text = "THÔNG TIN CÁ NHÂN";
            FInforGV fInforGV = new FInforGV(ms);
            OpenchildForm(fInforGV);
        }

        
        private void label11_Click(object sender, EventArgs e)
        {
            FAddAnounce fAddAnounce = new FAddAnounce(ms);
            fAddAnounce.ShowDialog();
            FGiangVien_Load(sender, e);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            string fileName = "Excel-icon.png";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Image image = Image.FromFile(imagePath);
            pictureBox10.Image = image;
            label10.Text = "ĐỀ TÀI";
            FProjetGV fProjetGV = new FProjetGV(ms);
            OpenchildForm(fProjetGV);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FReadLetterSV fReadLetterSV = new FReadLetterSV();

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string value1 = row.Cells["matb"].Value.ToString();

                // Gọi phương thức mới từ GIAOVIENDAO để lấy nội dung thông báo
                string noiDungThu = gvdao.DocThongBao(value1);

                // Hiển thị nội dung thông báo trong form FReadLetterSV
                fReadLetterSV.textBox1.Text = noiDungThu;
                OpenchildForm(fReadLetterSV);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            string fileName = "SEO-icon.png";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Image image = Image.FromFile(imagePath);
            pictureBox10.Image = image;
            label10.Text = "THEO DÕI";
            FFollow ffollow = new FFollow(ms);
            OpenchildForm(ffollow);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            string fileName = "Actions-mail-mark-task-icon.png";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Image image = Image.FromFile(imagePath);
            pictureBox10.Image = image;
            label10.Text = "KẾT QUẢ";
            FMarkGV fMarkGV = new FMarkGV(ms);
            OpenchildForm(fMarkGV);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
            FLogin fLogin = new FLogin();
            fLogin.ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            string fileName = "User-Group-icon.png";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Image image = Image.FromFile(imagePath);
            pictureBox10.Image = image;
            label10.Text = "THỐNG KÊ";
            FThongKe fThongKe = new FThongKe();
            OpenchildForm(fThongKe);
        }
    }
}