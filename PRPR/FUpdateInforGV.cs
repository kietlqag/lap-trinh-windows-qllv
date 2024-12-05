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
    public partial class FUpdateInforGV : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.Conn);
        CGiangVienDao gvdao = new CGiangVienDao();
        string ms;
        public FUpdateInforGV(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            string SDT = textBox1.Text;
            string Mail = textBox2.Text;
            string NoiO = textBox3.Text;
            string SDTNGTH = textBox4.Text;
            CGiangVien gv = new CGiangVien(ms, null, null, null, null, null, null, null, null, null, SDT, Mail, NoiO, SDTNGTH, null);
            gvdao.CapNhat(gv);
        }
    }
}
