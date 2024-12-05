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
    public partial class FUpdatePass : Form
    {
        LoginDao logindao = new LoginDao();
        string ms;
        public FUpdatePass(string ms)
        {
            InitializeComponent();
            this.ms = ms; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(textBox2.Text))
            {
                LoginDao.CapNhatMK(textBox2.Text, ms);
            }
            else
            {
                MessageBox.Show("Mật khẩu không khớp.");
            }
            this.Hide();
            FLogin fLogin = new FLogin();
            fLogin.Show();
        }
    }
}
