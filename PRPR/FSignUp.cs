using ControlLibrary;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PRPR
{
    public partial class FSignUp : Form
    {
        CSinhVienDao svdao = new CSinhVienDao();
        string ms;
        public FSignUp(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void FSignUp_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

        private void UC_LinkLabelClicked(object sender, EventArgs e)
        {
            UCDeTai ucDeTai = (UCDeTai)sender;
            string maDeTai = ucDeTai.GetMaDeTai();
            FDetailsSignUp fDetailsSignUp = new FDetailsSignUp(maDeTai);
            fDetailsSignUp.Show();
        }

        private void UC_LinkLabelClicked2(object sender, EventArgs e)
        {
            UCDeTai ucDeTai = (UCDeTai)sender;
            string maDeTai = ucDeTai.GetMaDeTai();
            try
            {
                DataTable dataTable = svdao.kiemtra1(ms, maDeTai);
                int count = Convert.ToInt32(dataTable.Rows[0][0]);
                if (count > 0)
                {
                    MessageBox.Show("Bạn đã đăng ký đề tài này.");
                    return;
                }
                else
                {
                    dataTable.Clear();
                    dataTable = svdao.kiemtra2(ms, maDeTai);
                    int countOtherTopic = Convert.ToInt32(dataTable.Rows[0][0]);

                    if (countOtherTopic > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn đã đăng ký một đề tài khác. Bạn có muốn đổi sang đề tài này không?", "Xác nhận", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                svdao.DoiDeTai(ms, maDeTai);
                                ucDeTai.SetTrangThai("Đã đăng ký");
                            }
                            catch
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi đổi đề tài.");
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            svdao.DoiDeTai2(ms, maDeTai);
                            ucDeTai.SetTrangThai("Đã đăng ký");
                        }
                        catch
                        {
                            MessageBox.Show("Đã đủ số lượng đăng ký.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

            flowLayoutPanel1.Controls.Clear();
            FSignUp_Load(null, EventArgs.Empty);
        }


        private void LoadDataFromDatabase()
        {
            DataTable dataTable = svdao.Load_DK();
            foreach (DataRow row in dataTable.Rows)
            {
                UCDeTai ucDeTai = new UCDeTai();

                ucDeTai.LinkLabelClicked += UC_LinkLabelClicked;
                ucDeTai.LinkLabelClicked2 += UC_LinkLabelClicked2;

                ucDeTai.SetMaDeTai(row["madetai"].ToString());
                ucDeTai.SetTenDeTai(row["tendetai"].ToString());
                ucDeTai.SetChuyenNganh(row["chuyennganh"].ToString());
                ucDeTai.SetGVHD(row["hoten"].ToString());

                bool check = CheckMSExists(ms, row["madetai"].ToString());

                // Cập nhật nội dung cho Label dựa trên kết quả kiểm tra
                if (check)
                {
                    ucDeTai.SetTrangThai("Đã đăng ký");
                }

                flowLayoutPanel1.Controls.Add(ucDeTai);
            }
        }

        private string previousSearchValue = ""; // Biến lưu trữ giá trị tìm kiếm trước đó
        private void label2_Click(object sender, EventArgs e)
        {
            string currentSearchValue = textBox1.Text; // Lấy giá trị tìm kiếm hiện tại

            if (currentSearchValue != previousSearchValue) // Chỉ thực hiện tìm kiếm nếu giá trị tìm kiếm khác với giá trị trước đó
            {
                flowLayoutPanel1.Controls.Clear(); // Xóa dữ liệu cũ
                LoadDataFromDatabase2();
                previousSearchValue = currentSearchValue; // Lưu giá trị tìm kiếm mới
            }
        }

        private DataTable GetDataFromDatabase2()
        {

            DataTable dataTable = null;
            string condition = null;

            if (comboBox1.Text == "Giảng viên")
                condition = "hoten";
            else if (comboBox1.Text == "Chuyên ngành")
                condition = "chuyennganh";
            else if (comboBox1.Text == "Ngôn ngữ")
                condition = "ngonngu";
            else if (comboBox1.Text == "Công nghệ")
                condition = "congnghe";

            dataTable = svdao.Load_DK2(condition, textBox1.Text);

            return dataTable;

        }

        private void LoadDataFromDatabase2()
        {
            DataTable dataTable = GetDataFromDatabase2();
            foreach (DataRow row in dataTable.Rows)
            {
                UCDeTai ucDeTai = new UCDeTai();

                ucDeTai.LinkLabelClicked += UC_LinkLabelClicked;
                ucDeTai.LinkLabelClicked2 += UC_LinkLabelClicked2;

                ucDeTai.SetMaDeTai(row["madetai"].ToString());
                ucDeTai.SetTenDeTai(row["tendetai"].ToString());
                ucDeTai.SetChuyenNganh(row["chuyennganh"].ToString());
                ucDeTai.SetGVHD(row["hoten"].ToString());

                bool check = CheckMSExists(ms, row["madetai"].ToString());

                // Cập nhật nội dung cho Label dựa trên kết quả kiểm tra
                if (check)
                {
                    ucDeTai.SetTrangThai("Đã đăng ký");
                }
                
                flowLayoutPanel1.Controls.Add(ucDeTai);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            LoadDataFromDatabase();
        }

        private bool CheckMSExists(string ms, string madetai)
        {
            DataTable dataTable = svdao.kiemtra3(ms, madetai);
            bool msExists = dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0][0]) > 0;
            return msExists;

        }
    }
}