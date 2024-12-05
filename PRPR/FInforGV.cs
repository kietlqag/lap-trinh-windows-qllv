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

namespace PRPR
{
    public partial class FInforGV : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        string ms;
        public FInforGV(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            FUpdateInforGV fUpdateInforGV = new FUpdateInforGV(ms) ;
            fUpdateInforGV.ShowDialog();
            FInforGV_Load(sender, e);
        }

        private void FInforGV_Load(object sender, EventArgs e)
        {
            DataTable dataTable = gvdao.Load_TTGV(ms);
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                customTextbox1.Text = row["hoten"].ToString();
                customTextbox2.Text = row["ngaysinh"].ToString().Substring(0,10);
                customTextbox3.Text = row["noisinh"].ToString();
                customTextbox4.Text = row["quequan"].ToString();
                customTextbox5.Text = row["dantoc"].ToString();
                customTextbox6.Text = row["chucvu"].ToString();
                customTextbox7.Text = row["loai"].ToString();
                customTextbox8.Text = row["hocvi"].ToString();
                customTextbox9.Text = row["khoa"].ToString();
                customTextbox10.Text = row["sdtngth"].ToString();
                customTextbox11.Text = row["mail"].ToString();
                customTextbox12.Text = row["noio"].ToString();
                customTextbox13.Text = row["sdt"].ToString();
            }
        }
    }
}
