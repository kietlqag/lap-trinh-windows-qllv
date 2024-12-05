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
using ControlLibrary;

namespace PRPR
{
    public partial class FAddAnounce : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        CSinhVienDao svdao = new CSinhVienDao();    
        string ms;
        public FAddAnounce(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (!radioButton1.Checked)
            {
                var selectedItem = comboBox1.SelectedItem;
                string masv = selectedItem.GetType().GetProperty("MSSV").GetValue(selectedItem).ToString();
                CThongBao tb = new CThongBao(textBox1.Text, textBox3.Text, ms, masv, null, null);
                gvdao.ThemThongBao(tb);
            }
            else
            {
                // Lấy danh sách sinh viên theo giảng viên (ms)
                DataTable sinhVienTable = svdao.GetSinhVienByGV(ms);

                foreach (DataRow row in sinhVienTable.Rows)
                {
                    string masv1 = row["Masv1"].ToString(); // Mã sinh viên 1
                    string masv2 = row["Masv2"].ToString(); // Mã sinh viên 2

                    // Tạo thông báo mới cho sinh viên 1
                    CThongBao tbForSinhVien1 = new CThongBao(textBox1.Text, textBox3.Text, ms, masv1, null, null);
                    // Gửi thông báo cho sinh viên 1
                    gvdao.ThemThongBao(tbForSinhVien1);

                    // Tạo thông báo mới cho sinh viên 2
                    CThongBao tbForSinhVien2 = new CThongBao(textBox1.Text, textBox3.Text, ms, masv2, null, null);
                    // Gửi thông báo cho sinh viên 2
                    gvdao.ThemThongBao(tbForSinhVien2);
                }
            }
        }

        private void FAddAnounce_Load(object sender, EventArgs e)
        {
            DataTable sinhVienTable = svdao.GetSinhVienByGV(ms);

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
