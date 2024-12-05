using ControlLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRPR
{
    internal class CGiangVienDao
    {
        
        DBConnection db = new DBConnection();
        public CGiangVienDao() {}

        // Lấy họ tên giảng viên
        public string GetHoTenByMaSo(string maso)
        {
            string sql = string.Format("SELECT hoten FROM ThongTinGiangVien WHERE ms = {0}", maso);
            SqlCommand command = new SqlCommand(sql, db.GetSqlConnection());
            db.GetSqlConnection().Open();
            string hoten = command.ExecuteScalar()?.ToString();
            db.GetSqlConnection().Close();
            return hoten;
        }

        
        public string GetMailByMaSo(string maso)
        {
            string sql = string.Format("SELECT mail FROM ThongTinGiangVien WHERE ms = {0}", maso);
            SqlCommand command = new SqlCommand(sql, db.GetSqlConnection());
            db.GetSqlConnection().Open();
            string mail= command.ExecuteScalar()?.ToString();
            db.GetSqlConnection().Close();
            return mail;
        }

        //Load thông báo
        public DataTable Load(string ms)
        {
            DataTable dtGiangVien = null;
            try
            {

                string sqlStr = string.Format("SELECT matb, tieude, hoten, thoidiem FROM ThongBao JOIN ThongTinSinhVien " +
                    "ON ThongBao.msnguoinhan = ThongTinSinhVien.ms WHERE msnguoigui = '{0}'", ms);
                dtGiangVien = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dtGiangVien;
        }

        //Load thông tin giảng viên
        public DataTable Load_TTGV(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT * FROM ThongTinGiangVien WHERE ms = '{0}'", ms);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        // Cập nhật thông tin giảng viên 
        public void CapNhat(CGiangVien gv)
        {
            try
            {
                string sql = string.Format("UPDATE ThongTinGiangVien SET sdt = '{0}', mail = '{1}', noio = N'{2}', sdtngth = '{3}' WHERE ms = '{4}'",
                    gv.SDT, gv.Mail, gv.NoiO, gv.SDTNGTH, gv.MS);

                db.ExcuteNonQuery(sql); // Sử dụng phương thức ExcuteNonQuery từ DBConnection để thực hiện câu lệnh SQL

                MessageBox.Show("Cập nhật thông tin thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thông tin thất bại." + ex.Message);
            }
        }


        //Đọc thông báo
        public string DocThongBao(string matb)
        {
            string noiDungThu = "";
            try
            {
                string sqlStr = string.Format("SELECT noidung FROM ThongBao WHERE matb = '{0}'", matb);
                DataTable dt = db.Excute(sqlStr);
                if (dt.Rows.Count > 0)
                {
                    noiDungThu = dt.Rows[0]["noidung"].ToString();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return noiDungThu;
        }

        //Thêm thông báo
        public void ThemThongBao(CThongBao tb)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                string sqlStr = string.Format("INSERT INTO ThongBao(tieude, noidung, msnguoigui, msnguoinhan, thoidiem) VALUES(N'{0}', N'{1}', '{2}', '{3}', '{4}')",
                    tb.Tieude, tb.Noidung, tb.Msnguoigui, tb.Msnguoinhan, dateTime.ToString());
                db.ExcuteNonQuery(sqlStr);
                    MessageBox.Show("Đã thêm thông báo mới.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi." + ex);
            }
        }

        //Load đề tài chung
        public DataTable Load_DTC()
        {
            DataTable dtdetai = null;
            try
            {
                string sqlStr = string.Format("SELECT madetai, tendetai N'Tên đề tài', chuyennganh N'Chuyên ngành', " +
                    "congnghe N'Công nghệ', chucnang N'Chức năng', ngonngu N'Ngôn ngữ', hoten N'Giảng viên hướng dẫn' " +
                    "FROM DeTai dt JOIN ThongTinGiangVien gv ON dt.gvhd = gv.ms");
                dtdetai = db.Excute(sqlStr);
                dtdetai.Columns.Remove("madetai");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dtdetai;
        }

        //Load đề tài riêng
        public DataTable Load_DTR(string ms)
        {
            DataTable dtdetai = null;
            try
            {
                string sqlStr = string.Format("SELECT madetai, tendetai N'Tên đề tài', chuyennganh N'Chuyên ngành', congnghe N'Công nghệ'," +
                    "chucnang N'Chức năng', ngonngu N'Ngôn ngữ', yeucau N'Yêu cầu', masv1 N'Mã sinh viên 1', masv2 N'Mã sinh viên 2'," +
                    "trangthai N'Trạng thái' FROM DeTai WHERE gvhd = '{0}'", ms);
                dtdetai = db.Excute(sqlStr);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dtdetai;
        }

        //Load đề tài khi ấn tìm kiếm
        public DataTable Load_DTTK(string filterColumn, string filterValue)
        {
            DataTable dtdetai = null;
            try
            {
                string sql = "SELECT madetai, tendetai N'Tên đề tài', chuyennganh N'Chuyên ngành', " +
                             "congnghe N'Công nghệ', chucnang N'Chức năng', ngonngu N'Ngôn ngữ', hoten N'Giảng viên hướng dẫn' " +
                             "FROM DeTai dt JOIN ThongTinGiangVien gv ON dt.gvhd = gv.ms";

                if (!string.IsNullOrEmpty(filterColumn) && !string.IsNullOrEmpty(filterValue))
                {
                    sql += " WHERE ";
                    switch (filterColumn)
                    {
                        case "Giảng viên":
                            sql += $"hoten = N'{filterValue}'";
                            break;
                        case "Chuyên ngành":
                            sql += $"chuyennganh = N'{filterValue}'";
                            break;
                        case "Công nghệ":
                            sql += $"congnghe = '{filterValue}'";
                            break;
                        case "Ngôn ngữ":
                            sql += $"ngonngu = '{filterValue}'";
                            break;
                    }
                }

                dtdetai = db.Excute(sql);
                dtdetai.Columns.Remove("madetai");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dtdetai;
        }

        //Cập nhật trạng thái đề tài
        public void CapNhatTT(string madetai)
        {
            string updateQuery = string.Format("UPDATE DeTai SET trangthai = N'Đã duyệt' WHERE madetai = '{0}'",madetai);
            db.ExcuteNonQuery(updateQuery);
        }

        //Thêm đề tài
        public void ThemDeTai(CDeTai dt)
        {
            try
            {
                string sqlStr = string.Format("INSERT INTO DeTai(tendetai, chuyennganh, congnghe, chucnang, ngonngu, gvhd, yeucau, trangthai, tiendo) " +
                    "VALUES(N'{0}', N'{1}', '{2}', N'{3}', '{4}', '{5}', N'{6}', N'{7}', {8})", dt.Tendetai, dt.Chuyennganh, dt.Congnghe, dt.Chucnang, 
                    dt.Ngonngu, dt.Gvhd, dt.Yeucau, dt.Trangthai, dt.Tiendo);
                    db.ExcuteNonQuery(sqlStr);
                    MessageBox.Show("Đã thêm đề tài mới.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi." + ex);
            }
        }

        //Load tiến độ
        public DataTable TienDo(string ms)
        {
            string sql = "SELECT madetai, tendetai, tiendo FROM DeTai WHERE gvhd = @gvhdName";
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = db.GetSqlConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@gvhdName", ms); 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            return dataTable;
        }

        //Load chi tiết tiến độ
        public DataTable ChiTietTienDo(string madetai)
        {
            string sql = "SELECT tieude N'Tiêu đề', noidung N'Nội dung', hoten N'Người nhận', ngayhethan N'Ngày hết hạn'," +
                "trangthai N'Trạng thái' FROM NhiemVu nv JOIN ThongTinSinhVien ttsv ON nv.msnhan = ttsv.ms WHERE madetai = @madetai";
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = db.GetSqlConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@madetai", madetai);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            return dataTable;
        }

        //Load điểm
        public DataTable Load_Diem(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = String.Format("SELECT sv.ms AS 'Mã số sinh viên', sv.hoten AS 'Họ tên', sv.lop AS 'Lớp', " +
                                "sv.diem AS 'Điểm' FROM ThongTinSinhVien sv " +
                                "LEFT JOIN DeTai dt1 ON sv.ms = dt1.masv1 " +
                                "LEFT JOIN DeTai dt2 ON sv.ms = dt2.masv2 " +
                                $"WHERE dt1.gvhd = '{ms}' OR dt2.gvhd = '{ms}'");

                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        public DataTable Load_Diem2(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT ms N'Mã số sinh viên',hoten N'Họ tên', lop N'Lớp', diem N'Điểm' " +
                    "FROM ThongTinSinhVien WHERE ms = '{0}'", ms); ;
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        public DataTable Load_Diem3(string hoten)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT ms N'Mã số sinh viên',hoten N'Họ tên', lop N'Lớp', diem N'Điểm' " +
                     "FROM ThongTinSinhVien WHERE hoten = N'{0}'", hoten);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }


        //Cập nhật điểm
        public void CapNhapDiem(double diem, string ms)
        {
            try
            {
                string sqlStr = String.Format("UPDATE ThongTinSinhVien SET diem = {0} WHERE ms = '{1}'", diem, ms);
                db.ExcuteNonQuery(sqlStr);
                MessageBox.Show("Đã lưu điểm.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi." + ex);
            }
        }

        //Thêm nhiệm vụ
        public void ThemNhiemVu(CNhiemVu nv)
        {
            string sqlStr = string.Format("INSERT INTO NhiemVu(tieude, noidung, msgui, msnhan, ngayhethan, trangthai, madetai) VALUES(N'{0}', N'{1}', '{2}', '{3}', '{4}', N'{5}', '{6}')",
                    nv.GetTieuDe(), nv.GetNoiDung(), nv.GetMsgGui(), nv.GetMsgNhan(), nv.GetNgayHetHan(), nv.GetTrangThai(), nv.GetMaDeTai());
            db.ExcuteNonQuery(sqlStr);

            MessageBox.Show("Đã thêm nhiệm vụ mới.");

            string sqlCountTasks = string.Format("SELECT COUNT(*) FROM NhiemVu WHERE madetai = '{0}'", nv.GetMaDeTai());
            string sqlCountCompletedTasks = string.Format("SELECT COUNT(*) FROM NhiemVu WHERE madetai = '{0}' AND trangthai = N'{1}'", nv.GetMaDeTai(), "Hoàn thành");

            // Thực hiện truy vấn để đếm số lượng nhiệm vụ và số lượng nhiệm vụ đã hoàn thành
            DataTable dtCountTasks = db.Excute(sqlCountTasks);
            DataTable dtCountCompletedTasks = db.Excute(sqlCountCompletedTasks);

            // Lấy số lượng nhiệm vụ và số lượng nhiệm vụ đã hoàn thành từ các bảng dữ liệu
            int task = Convert.ToInt32(dtCountTasks.Rows[0][0]);
            int taskdone = Convert.ToInt32(dtCountCompletedTasks.Rows[0][0]);

            // Tính toán tiến độ
            int tiendo = 100 * taskdone / task;

            // Cập nhật tiến độ vào bảng DeTai
            string sqlUpdateProgress = string.Format("UPDATE DeTai SET tiendo = {0} WHERE madetai = '{1}'", tiendo, nv.GetMaDeTai());
            db.ExcuteNonQuery(sqlUpdateProgress);
        }

        //Thống kê
        public DataTable ThongKe()
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT tiendo FROM DeTai");
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        public DataTable ThongKe2()
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT diem FROM ThongTinSinhVien");
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }
    }

}
