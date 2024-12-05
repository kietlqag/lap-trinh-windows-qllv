using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace PRPR
{
    public partial class FGetMail : Form
    {
        Random random = new Random();
        int otp;
        string ms;
        CSinhVienDao svdao = new CSinhVienDao();
        CGiangVienDao gvdao = new CGiangVienDao();

        public FGetMail(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }


        private static bool ValidateEmailFormat(string email)
        {
            // Mẫu regular expression cho định dạng email
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Kiểm tra khớp mẫu
            bool isMatch = Regex.IsMatch(email, pattern);

            return isMatch;
        }
        private void FGetMail_Load(object sender, EventArgs e)
        {
            string email = svdao.GetMailByMaSo(ms);
            if (email == null)
            {
                email = gvdao.GetMailByMaSo(ms);
            }
            textBox1.Text = email;
        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            string email = svdao.GetMailByMaSo(ms);
            if(email == null) {
                email = gvdao.GetMailByMaSo(ms);
                if( email == null ) {
                    MessageBox.Show("Không tồn tại email");
                }
            }
            bool isValidEmail = ValidateEmailFormat(email);
            if (isValidEmail)
            {
                try
                {
                    otp = random.Next(100000, 1000000);
                    var fromAddress = new MailAddress("quockiet3304@gmail.com");
                    var toAddress = new MailAddress(email);
                    const string frompass = "sypuxkttqrxxbrny";
                    const string subject = "OTP code";
                    string body = "Đây là mã OTP của bạn dùng để cập nhật lại mật khẩu: " + otp.ToString();

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, frompass),
                        Timeout = 200000
                    };

                    using (var message = new MailMessage(fromAddress, toAddress))
                    {
                        message.Subject = subject;
                        message.Body = body;

                        smtp.Send(message);
                    }

                    MessageBox.Show("Mã OTP đã được gửi. Vui lòng kiểm tra email của bạn.");
                    this.Hide();
                    FGetOTP fGetOTP = new FGetOTP(otp, ms);
                    fGetOTP.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập lại.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FLogin fLogin = new FLogin();
            fLogin.ShowDialog();
        }
    }
}