using ControlLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRPR
{
    public partial class FDetailsSignUp : Form
    {
        CSinhVienDao svdao = new CSinhVienDao();
        string madetai;
        public FDetailsSignUp(string madetai)
        {
            InitializeComponent();
            this.madetai = madetai;
            LoadDataFromDatabase();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetUCSVDangKy(string masv)
        {
            DataTable dataTable = svdao.Load3(masv);
            foreach (DataRow row in dataTable.Rows)
            {
                UCsinhviendangky uCsinhviendangky = new UCsinhviendangky();

                uCsinhviendangky.SetMSSV(row["ms"].ToString());
                uCsinhviendangky.SetHoTen(row["hoten"].ToString());
                uCsinhviendangky.SetLop(row["lop"].ToString());

                flowLayoutPanel1.Controls.Add(uCsinhviendangky);
            }
        }

        private void LoadDataFromDatabase()
        {
            DataTable dataTable = svdao.Load(madetai);
            DataTable dataTable2 = svdao.Load1(madetai);
            int dem = 0;
            foreach (DataRow row in dataTable2.Rows)
            {
                // Lấy giá trị từ cột "masv1" và "masv2" từ DataTable
                string masv1 = row["masv1"].ToString();
                string masv2 = row["masv2"].ToString();

                if (!string.IsNullOrEmpty(masv1))
                {
                    dem++;
                    GetUCSVDangKy(masv1);
                }

                if (!string.IsNullOrEmpty(masv2))
                {
                    dem++;
                    GetUCSVDangKy(masv2);
                }
            }

            foreach (DataRow row in dataTable.Rows)
            {
                // Hiển thị các giá trị từ DataTable lên các TextBox và ProgressBar
                textBox1.Text = row["tendetai"].ToString();
                textBox2.Text = row["chuyennganh"].ToString();
                textBox3.Text = row["congnghe"].ToString();
                textBox4.Text = row["chucnang"].ToString();
                textBox5.Text = row["gvhd"].ToString();
                textBox6.Text = row["ngonngu"].ToString();
                textBox7.Text = row["yeucau"].ToString();
                progressBar1.Value = Convert.ToInt32(row["tiendo"]);
            }
            label2.Text = dem.ToString();
        }
    }
}
