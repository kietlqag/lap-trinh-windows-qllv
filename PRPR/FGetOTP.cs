using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRPR
{
    public partial class FGetOTP : Form
    {
        string ms;
        private int otp;
        public FGetOTP(int otp, string ms)
        {
            InitializeComponent();
            this.otp = otp;
            this.ms = ms;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(otp.ToString().Equals(textBox1.Text))
            {
                MessageBox.Show("Xác thực thành công. Vui lòng cập nhật lại mật khẩu");
                this.Hide();
                FUpdatePass fUpdatePass = new FUpdatePass(ms);
                fUpdatePass.Show();
            }
            else
            {
                MessageBox.Show("Mã xác thực không hợp lệ.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FGetMail fGetMail = new FGetMail(ms);
            fGetMail.Show();
        }
    }
}
