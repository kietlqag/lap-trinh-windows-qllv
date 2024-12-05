using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRPR
{
    internal class LoginDao
    {
        private static DBConnection db = new DBConnection();
        public LoginDao() {}

        //Kiểm tra
        public DataTable KiemTra1(string ms)
        {
            DataTable dataTable = null;
            string sqlStr = string.Format("SELECT COUNT(*) AS Count FROM DangNhap WHERE ms = '{0}'", ms);
            dataTable = db.Excute(sqlStr);
            return dataTable;
        }

        //Lấy mật khẩu
        public DataTable MatKhau(string ms)
        {
            DataTable dataTable = null;
            string sqlStr = string.Format("SELECT matkhau, quyen, salt FROM DangNhap WHERE ms = '{0}'", ms);
            dataTable = db.Excute(sqlStr);
            return dataTable;
        }

        // Tạo Salt ngẫu nhiên
        private static string GenerateSalt(int length = 32)
        {
            byte[] saltBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // Mã hóa mật khẩu kết hợp với Salt bằng SHA-256
        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Kết hợp mật khẩu và salt
                string saltedPassword = password + salt;
                byte[] passwordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static void CapNhatMK(string mkmoi, string ms)
        {
            try
            {
                // Tạo Salt mới
                string salt = GenerateSalt();

                // Mã hóa mật khẩu mới với Salt
                string hashedPassword = HashPassword(mkmoi, salt);

                // Cập nhật mật khẩu vào cơ sở dữ liệu với Salt
                string sqlStr = "UPDATE DangNhap SET matkhau = @matkhau, salt = @salt WHERE ms = @ms";
                SqlCommand cmd = new SqlCommand(sqlStr, db.GetSqlConnection());
                cmd.Parameters.AddWithValue("@matkhau", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt);  // Lưu salt vào cơ sở dữ liệu
                cmd.Parameters.AddWithValue("@ms", ms);      // Mã sinh viên hoặc mã người dùng
                db.ExcuteNonQueryCmd(cmd);

                MessageBox.Show("Cập nhật mật khẩu thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại: " + ex.Message);
            }
        }
    }
}
