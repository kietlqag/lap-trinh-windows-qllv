using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRPR
{
    internal class CNhiemVu
    {
        string tieude;
        string noidung;
        string msgui;
        string msnhan;
        string ngayhethan;
        string trangthai;
        string madetai;

        public CNhiemVu(string tieude, string noidung, string msgui, string msnhan, string ngayhethan, string trangthai, string madetai)
        {
            this.tieude = tieude;
            this.noidung = noidung;
            this.msgui = msgui;
            this.msnhan = msnhan;
            this.ngayhethan = ngayhethan;
            this.trangthai = trangthai;
            this.madetai = madetai;
        }

        // Phương thức getter
        public string GetTieuDe()
        {
            return tieude;
        }

        public string GetNoiDung()
        {
            return noidung;
        }

        public string GetMsgGui()
        {
            return msgui;
        }

        public string GetMsgNhan()
        {
            return msnhan;
        }

        public string GetNgayHetHan()
        {
            return ngayhethan;
        }

        public string GetTrangThai()
        {
            return trangthai;
        }

        public string GetMaDeTai()
        {
            return madetai;
        }
    }
}
