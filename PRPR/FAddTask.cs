using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRPR
{
    public partial class FAddTask : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        CSinhVienDao svdao = new CSinhVienDao();
        string ms;
        string madetai;
        public FAddTask(string ms, string madetai)
        {
            InitializeComponent();
            this.ms = ms;
            this.madetai = madetai; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var selectedItem = comboBox1.SelectedItem;
            string masv = selectedItem.GetType().GetProperty("MSSV").GetValue(selectedItem).ToString();
            CNhiemVu nv = new CNhiemVu(textBox1.Text, textBox3.Text, ms, masv, dateTimePicker1.Value.ToString(), "Chưa hoàn thành", madetai);
            gvdao.ThemNhiemVu(nv);
        }

        private void FAddTask_Load(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ cơ sở dữ liệu bằng hàm GetSinhVienByKhoa
            DataTable sinhVienTable = svdao.GetSinhVienByMDT(madetai);

            // Xóa hết dữ liệu hiện tại trong ComboBox (nếu có)
            comboBox1.Items.Clear();

            foreach (DataRow row in sinhVienTable.Rows)
            {
                string tenSinhVien1 = row["TenSinhVien1"].ToString();
                string tenSinhVien2 = row["TenSinhVien2"].ToString();
                string masv1 = row["Masv1"].ToString();
                string masv2 = row["Masv2"].ToString();

                // Thêm tên sinh viên và gắn mã sinh viên vào Tag
                comboBox1.Items.Add(new { Name = tenSinhVien1, MSSV = masv1 });
                comboBox1.Items.Add(new { Name = tenSinhVien2, MSSV = masv2 });
            }

        }
    }
}
