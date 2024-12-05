using ControlLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRPR
{
    internal class CSinhVienDao
    {
        DBConnection db = new DBConnection();
        public CSinhVienDao() { }



        // Lấy họ tên sinh viên
        public string GetHoTenByMaSo(string maso)
        {
            string sql = string.Format("SELECT hoten FROM ThongTinSinhVien WHERE ms = {0}", maso);
            SqlCommand command = new SqlCommand(sql, db.GetSqlConnection());
            db.GetSqlConnection().Open();
            string hoten = command.ExecuteScalar()?.ToString();
            db.GetSqlConnection().Close();
            return hoten;
        }

        public string GetMailByMaSo(string maso)
        {
            string sql = string.Format("SELECT email FROM ThongTinSinhVien WHERE ms = {0}", maso);
            SqlCommand command = new SqlCommand(sql, db.GetSqlConnection());
            db.GetSqlConnection().Open();
            string mail = command.ExecuteScalar()?.ToString();
            db.GetSqlConnection().Close();
            return mail;
        }

        // Hàm lấy danh sách sinh viên theo đề tài
        public DataTable GetSinhVienByMDT(string maso)
        {
            string sql = string.Format("SELECT Masv1, masv2, SV1.hoten AS TenSinhVien1, SV2.hoten AS TenSinhVien2 FROM  DeTai DT LEFT JOIN  ThongTinSinhVien SV1 ON DT.Masv1 = SV1.ms LEFT JOIN ThongTinSinhVien SV2 ON DT.Masv2 = SV2.ms WHERE madetai = {0}", maso);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, db.GetSqlConnection());
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        // Hàm lấy danh sách sinh viên theo giảng viên
        public DataTable GetSinhVienByGV(string maso)
        {
            string sql = string.Format("SELECT Masv1, masv2, SV1.hoten AS TenSinhVien1, SV2.hoten AS TenSinhVien2 FROM DeTai DT LEFT JOIN ThongTinSinhVien SV1 ON DT.Masv1 = SV1.ms LEFT JOIN ThongTinSinhVien SV2 ON DT.Masv2 = SV2.ms WHERE Gvhd = {0}", maso);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, db.GetSqlConnection());
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        // Cập nhật thông tin sinh viên 
        public void CapNhatTTSV(CSinhVien sv)
        {
            try
            {
                string sql = string.Format("UPDATE ThongTinSinhVien SET sdt = '{0}', email = '{1}', noio = N'{2}', sdtnt = '{3}' WHERE ms = '{4}'",
                    sv.SDT, sv.Email, sv.NoiO, sv.SDTNT, sv.MS);

                db.ExcuteNonQuery(sql);

                MessageBox.Show("Cập nhật thông tin thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thông tin thất bại." + ex.Message);
            }
        }

        //Cập nhật trạng thái nhiệm vụ
        public DataTable CapNhatTrangThaiNhiemVu(string manv)
        {
            DataTable dataTable = null;
            string sqlStr = string.Format("SELECT trangthai FROM NhiemVu WHERE manv = {0}", manv);
            dataTable = db.Excute(sqlStr);
            return dataTable;
        }

        public void CapNhatTTHT(string manv)
        {
            string sqlStr = string.Format("UPDATE NhiemVu SET trangthai = N'Hoàn thành' WHERE manv = {0}", manv);
            db.ExcuteNonQuery(sqlStr);
        }

        public void CapNhatTTCHT(string manv)
        {
            string sqlStr = string.Format("UPDATE NhiemVu SET trangthai = N'Chưa hoàn thành' WHERE manv = {0}", manv);
            db.ExcuteNonQuery(sqlStr);
        }

        //Load chi tiết báo cáo
        public DataTable Load_BaoCao(string manv)
        {
            DataTable dataTable = null;
            string sqlStr = string.Format("SELECT trangthai FROM NhiemVu WHERE manv = {0}", manv);
            dataTable = db.Excute(sqlStr);
            return dataTable;
        }

        //Cập nhật tiến độ
        public void CapNhatTienDo(string madetai)
        {
            string sqlCountTasks = string.Format("SELECT COUNT(*) FROM NhiemVu WHERE madetai = '{0}'", madetai);
            string sqlCountCompletedTasks = string.Format("SELECT COUNT(*) FROM NhiemVu WHERE madetai = '{0}' AND trangthai = N'{1}'",madetai, "Hoàn thành");

            // Thực hiện truy vấn để đếm số lượng nhiệm vụ và số lượng nhiệm vụ đã hoàn thành
            DataTable dtCountTasks = db.Excute(sqlCountTasks);
            DataTable dtCountCompletedTasks = db.Excute(sqlCountCompletedTasks);

            // Lấy số lượng nhiệm vụ và số lượng nhiệm vụ đã hoàn thành từ các bảng dữ liệu
            int task = Convert.ToInt32(dtCountTasks.Rows[0][0]);
            int taskdone = Convert.ToInt32(dtCountCompletedTasks.Rows[0][0]);

            // Tính toán tiến độ
            int tiendo = 100 * taskdone / task;

            // Cập nhật tiến độ vào bảng DeTai
            string sqlUpdateProgress = string.Format("UPDATE DeTai SET tiendo = {0} WHERE madetai = '{1}'", tiendo, madetai);
            db.ExcuteNonQuery(sqlUpdateProgress);
        }

        //Load chi tiết đề tài
        public DataTable Load(string madetai)
        {
            DataTable dataTable = null;
            string sqlStr = string.Format("SELECT tendetai, chuyennganh, congnghe, chucnang, ngonngu, gvhd, yeucau, tiendo FROM DeTai WHERE madetai = '{0}'", madetai);
            dataTable = db.Excute(sqlStr);
            return dataTable;
        }
        public DataTable Load1(string madetai)
        {
            DataTable dataTable = null;
            string sqlStr = string.Format("SELECT masv1, masv2 FROM DeTai WHERE madetai = '{0}'", madetai);
            dataTable = db.Excute(sqlStr);
            return dataTable;
        }

        //Load sinh viên đăng ký
        public DataTable Load3(string ms)
        {
            DataTable dataTable = null;
            string sqlStr = string.Format("SELECT ms, hoten, lop FROM ThongTinSinhVien WHERE ms = '{0}'", ms);
            dataTable = db.Excute(sqlStr);
            return dataTable;
        }

        //Load thông tin sinh viên
        public DataTable Load_TTSV(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT * FROM ThongTinSinhVien WHERE ms = '{0}'", ms);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        //Xem điểm
        public DataTable XemDiem(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT diem FROM ThongTinSinhVien WHERE ms = '{0}'", ms);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        //Load bảng nhiệm vụ được giao
        public DataTable NhiemVuDuocGiao(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format(@"
                SELECT NhiemVu.manv, NhiemVu.tieude, 
                       COALESCE(ThongTinSinhVien.hoten, ThongTinGiangVien.hoten) AS hoten, 
                       NhiemVu.ngayhethan, NhiemVu.trangthai
                FROM NhiemVu
                LEFT JOIN ThongTinSinhVien ON NhiemVu.msgui = ThongTinSinhVien.ms
                LEFT JOIN ThongTinGiangVien ON NhiemVu.msgui = ThongTinGiangVien.ms
                WHERE NhiemVu.msnhan = '{0}'", ms);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        //Load bảng nhiệm vụ đã giao
        public DataTable NhiemVuDaGiao(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT manv, tieude N'Tiêu đề', noidung N'Nội dung', hoten N'Người nhận', " +
                    "ngayhethan N'Thời hạn', trangthai N'Trạng thái' FROM NhiemVu nv JOIN ThongTinSinhVien sv ON nv.msnhan = sv.ms WHERE msgui = '{0}'", ms);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        //Cell click bảng nhiệm vụ
        public DataTable NhiemVuChiTiet(string value1)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT noidung FROM NhiemVu WHERE manv = '{0}'", value1);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        // Kiểm tra xem người dùng đã đăng ký đề tài này hay chưa
        public DataTable kiemtra1(string ms, string madetai)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT COUNT(*) FROM DeTai WHERE (masv1 = '{0}' OR masv2 = '{0}') AND maDeTai = '{1}'", ms, madetai);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        // Kiểm tra xem người dùng đã đăng ký đề tài khác chưa
        public DataTable kiemtra2(string ms, string madetai)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT COUNT(*) FROM DeTai WHERE (masv1 = '{0}' OR masv2 = '{0}') AND maDeTai != '{1}'", ms, madetai);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        // Kiểm tra xem người dùng đã đăng ký đề tài khác chưa

        public void DoiDeTai(string ms, string madetai)
        {
            try
            {
                string sqlStr = string.Format("UPDATE DeTai SET masv1 = CASE " +
                                "WHEN masv1 = '{0}' THEN NULL ELSE masv1 END, " +
                                "masv2 = CASE " +
                                "WHEN masv2 = '{0}' THEN NULL ELSE masv2 END " +
                                "WHERE (masv1 = '{0}' OR masv2 = '{0}') AND maDeTai != '{1}'; " +
                                "UPDATE DeTai SET masv1 = CASE " +
                                "WHEN masv1 IS NULL AND maDeTai = '{1}' THEN '{0}' " +
                                "ELSE masv1 END, " +
                                "masv2 = CASE " +
                                "WHEN masv2 IS NULL AND masv1 IS NOT NULL AND maDeTai = '{1}' THEN '{0}'" +
                                "ELSE masv2 END", ms, madetai);
                db.ExcuteNonQuery(sqlStr);
                MessageBox.Show("Đổi thành công.");
            }
            catch { }
            
        }

        public void DoiDeTai2(string ms, string madetai)
        {
            try
            {
                string sqlStr = string.Format("UPDATE DeTai SET masv1 = CASE " +
                            "WHEN masv1 IS NULL AND maDeTai = '{1}' THEN '{0}' " +
                            "ELSE masv1 END, " +
                            "masv2 = CASE " +
                            "WHEN masv2 IS NULL AND masv1 IS NOT NULL AND maDeTai = '{1}' THEN '{0}' " +
                            "ELSE masv2 END", ms, madetai);
                db.ExcuteNonQuery(sqlStr);
                MessageBox.Show("Đăng ký thành công.");
            }
            catch { }
        }

        //Load đăng ký
        public DataTable Load_DK()
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT madetai, tendetai, chuyennganh, hoten FROM DeTai dt JOIN ThongTinGiangVien gv ON dt.gvhd = gv.ms");
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        public DataTable Load_DK2(string condition, string text)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT madetai, tendetai, chuyennganh, hoten FROM DeTai dt JOIN ThongTinGiangVien gv ON dt.gvhd = gv.ms WHERE {0} = N'{1}'", condition, text);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        //Kiểm tra
        public DataTable kiemtra3(string ms, string madetai)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT COUNT(*) FROM DeTai WHERE (masv1 = '{0}' OR masv2 = '{0}') AND madetai = '{1}'", ms, madetai);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        //Load thông báo
        public DataTable ThongBao(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT matb, tieude, hoten, thoidiem FROM ThongBao JOIN ThongTinGiangVien ON ThongBao.msnguoigui = ThongTinGiangVien.ms WHERE msnguoinhan = '{0}'", ms);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
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

        public DataTable KTThongBao(string ms)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = string.Format("SELECT madetai FROM DeTai WHERE (masv1 = '{0}' OR masv2 = '{0}') AND trangthai = N'Đã duyệt'", ms);
                dt = db.Excute(sqlStr);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return dt;
        }

        public async Task SaveFileToDatabase(string fileName, byte[] fileData, string mdt)
        {
            // Mở kết nối mới cho mỗi lần gọi
            using (SqlConnection connection = db.GetSqlConnection())
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa (theo madetai)
                string checkQuery = "SELECT COUNT(1) FROM SanPham WHERE madetai = @MDT";

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@MDT", mdt);

                    int recordCount = (int)await checkCommand.ExecuteScalarAsync();

                    if (recordCount > 0)
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện cập nhật
                        string updateQuery = "UPDATE SanPham SET fileData = @FileData, fileName = @FileName WHERE madetai = @MDT";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@FileData", fileData);
                            updateCommand.Parameters.AddWithValue("@FileName", fileName);
                            updateCommand.Parameters.AddWithValue("@MDT", mdt);  // Đừng quên thêm parameter cho madetai

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi chưa tồn tại, thực hiện chèn mới
                        string insertQuery = "INSERT INTO SanPham (fileName, fileData, madetai) VALUES (@FileName, @FileData, @MDT)";

                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@FileName", fileName);
                            insertCommand.Parameters.AddWithValue("@FileData", fileData);
                            insertCommand.Parameters.AddWithValue("@MDT", mdt); // Đừng quên thêm parameter cho madetai

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
        }

        public async Task SaveSanPhamDetailsAsync(string madetai, string tieude, string noidung, DateTime thoigiannop, string msnguoinop, string fileName)
        {
            try
            {
                if (madetai == null)
                {
                    MessageBox.Show("Vui lòng chọn mã đề tài!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Vui lòng chọn file phù hợp trước khi lưu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = db.GetSqlConnection())
                {
                    await connection.OpenAsync();

                    // Kiểm tra xem mã đề tài đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(1) FROM SanPham WHERE madetai = @madetai";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@madetai", madetai);

                        int recordExists = (int)await checkCommand.ExecuteScalarAsync();

                        if (recordExists > 0)
                        {
                            // Nếu mã đề tài đã tồn tại, thực hiện cập nhật
                            string updateQuery = @"UPDATE SanPham 
                                           SET tieude = @tieude, 
                                               noidung = @noidung, 
                                               thoigiannop = @thoigiannop, 
                                               msnguoinop = @msnguoinop
                                           WHERE madetai = @madetai";

                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@tieude", tieude);
                                updateCommand.Parameters.AddWithValue("@noidung", noidung);
                                updateCommand.Parameters.AddWithValue("@thoigiannop", thoigiannop);
                                updateCommand.Parameters.AddWithValue("@msnguoinop", msnguoinop);
                                updateCommand.Parameters.AddWithValue("@madetai", madetai);

                                await updateCommand.ExecuteNonQueryAsync();
                                MessageBox.Show("Thông tin đã được cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            // Nếu mã đề tài không tồn tại, hiển thị thông báo lỗi
                            MessageBox.Show("Không tìm thấy mã đề tài, không thể cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
