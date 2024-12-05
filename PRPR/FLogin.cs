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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace PRPR
{
    public partial class FLogin : Form
    {
        LoginDao logindao = new LoginDao();
        public FLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                // Kiểm tra nếu tên đăng nhập tồn tại
                DataTable dataTable = logindao.KiemTra1(textBox1.Text);

                if (dataTable.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(dataTable.Rows[0]["Count"]);
                    if (count > 0)
                    {
                        // Lấy thông tin mật khẩu và salt từ cơ sở dữ liệu
                        DataTable dataTable1 = logindao.MatKhau(textBox1.Text);
                        if (dataTable1.Rows.Count > 0)
                        {
                            string hashedPassword = dataTable1.Rows[0]["matkhau"].ToString(); // Mật khẩu đã mã hóa
                            string salt = dataTable1.Rows[0]["salt"].ToString();             // Salt
                            string quyen = dataTable1.Rows[0]["quyen"].ToString();          // Quyền truy cập

                            // Gọi hàm HashPassword từ LoginDao để mã hóa mật khẩu nhập vào
                            string inputPassword = textBox2.Text; // Mật khẩu người dùng nhập
                            string hashedInputPassword = LoginDao.HashPassword(inputPassword, salt); // Mã hóa mật khẩu nhập

                            // So sánh mật khẩu
                            if (hashedPassword == hashedInputPassword)
                            {
                                // Kiểm tra quyền truy cập
                                if (quyen == "Sinh viên")
                                {
                                    if (radioButton1.Checked)
                                    {
                                        FSinhVien fSinhVien = new FSinhVien(textBox1.Text);
                                        this.Hide();
                                        fSinhVien.Show();
                                    }
                                    else if (radioButton2.Checked)
                                    {
                                        MessageBox.Show("Bạn không có quyền truy cập vào giao diện giáo viên.");
                                    }
                                }
                                else if (quyen == "Giảng viên")
                                {
                                    if (radioButton1.Checked)
                                    {
                                        MessageBox.Show("Bạn không có quyền truy cập vào giao diện sinh viên.");
                                    }
                                    else if (radioButton2.Checked)
                                    {
                                        FGiangVien fGiangVien = new FGiangVien(textBox1.Text);
                                        this.Hide();
                                        fGiangVien.Show();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mật khẩu không đúng.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập không tồn tại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản (Sinh viên hoặc Giảng viên).");
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dataTable = logindao.KiemTra1(textBox1.Text);
            if (dataTable.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dataTable.Rows[0]["Count"]);
                if (count > 0)
                {
                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        this.Hide();
                        FGetMail fGetMail = new FGetMail(textBox1.Text);
                        fGetMail.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập tên đăng nhập.");
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại.");
                }
            }

        }
    }
}