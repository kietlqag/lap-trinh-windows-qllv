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
    public partial class FUpdateInfor : Form
    {
        CSinhVienDao svdao = new CSinhVienDao();
        string ms;
        public FUpdateInfor(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void customButton1_Click_1(object sender, EventArgs e)
        {
            CSinhVien sv = new CSinhVien(ms, null, null, null, null, null, null, null, null, null, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, null);
            svdao.CapNhatTTSV(sv);
        }
    }
}
