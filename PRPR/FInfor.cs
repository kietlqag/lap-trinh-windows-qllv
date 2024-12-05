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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRPR
{
    public partial class FInfor : Form
    {
        CSinhVienDao svdao = new CSinhVienDao();
        string ms;
        public FInfor(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void FInfor_Load(object sender, EventArgs e)
        {
            DataTable dataTable = svdao.Load_TTSV(ms);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0]; // Lấy dòng đầu tiên từ DataTable

                customTextbox1.Text = row["hoten"].ToString();
                customTextbox2.Text = row["ngaysinh"].ToString().Substring(0,10);
                customTextbox3.Text = row["noisinh"].ToString();
                customTextbox4.Text = row["quequan"].ToString();
                customTextbox5.Text = row["dantoc"].ToString();
                customTextbox6.Text = row["tinhtrang"].ToString();
                customTextbox7.Text = row["hinhthuc"].ToString();
                customTextbox8.Text = row["lop"].ToString();
                customTextbox9.Text = row["khoahoc"].ToString();
                customTextbox10.Text = row["sdtnt"].ToString();
                customTextbox11.Text = row["email"].ToString();
                customTextbox12.Text = row["noio"].ToString();
                customTextbox13.Text = row["sdt"].ToString();
            }
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            FUpdateInfor fUpdateInfor = new FUpdateInfor(ms);
            fUpdateInfor.ShowDialog();
            FInfor_Load(sender, e);
        }
    }
}
