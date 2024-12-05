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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRPR
{
    public partial class FDetailsReport : Form
    {
        CSinhVienDao svdao = new CSinhVienDao();
        string manv;
        string madetai;
        string noidung;
        public FDetailsReport(string manv, string madetai, string noidung)
        {
            InitializeComponent();
            this.manv = manv;
            this.madetai = madetai;
            this.noidung = noidung;
        }

        private void customButton1_Click(object sender, EventArgs e)
        {

            DataTable dataTable = svdao.CapNhatTrangThaiNhiemVu(manv);

            if (dataTable.Rows.Count > 0)
            {
                string trangThai = dataTable.Rows[0]["trangthai"].ToString();

                // Kiểm tra trạng thái hiện tại
                if (trangThai == "Chưa hoàn thành")
                {
                    // Cập nhật trạng thái và đổi text button
                    svdao.CapNhatTTHT(manv);
                    customButton1.Text = "Chưa hoàn thành";
                }
                else if (trangThai == "Hoàn thành")
                {
                    // Cập nhật trạng thái và đổi text button
                    svdao.CapNhatTTCHT(manv);
                    customButton1.Text = "Hoàn thành";
                }
            }
            UpdateProcess();
        }

        private void FDetailsReport_Load(object sender, EventArgs e)
        {
            textBox1.Text = noidung;

            DataTable dataTable = svdao.Load_BaoCao(manv);

            if (dataTable.Rows.Count > 0)
            {
                string trangThai = dataTable.Rows[0]["trangthai"].ToString();

                if (trangThai == "Chưa hoàn thành")
                {
                    customButton1.Text = "Hoàn thành";
                }
                else if (trangThai == "Hoàn thành")
                {
                    customButton1.Text = "Chưa hoàn thành";
                }
            }
        }

        private void UpdateProcess()
        {
            svdao.CapNhatTienDo(madetai);
        }
    }
}
