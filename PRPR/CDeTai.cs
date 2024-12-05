using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRPR
{
    internal class CDeTai
    {
        public string Tendetai { get; }
        public string Chuyennganh { get; }
        public string Congnghe { get; }
        public string Chucnang { get; }
        public string Ngonngu { get; }
        public string Gvhd { get; }
        public string Masv1 { get; }
        public string Masv2 { get; }
        public string Yeucau { get; }
        public string Trangthai { get; }
        public int Tiendo { get; }

        public CDeTai(string tendetai, string chuyennganh, string congnghe, string chucnang, string ngonngu, string gvhd, string masv1, string masv2, string yeucau, string trangthai, int tiendo)
        {
            this.Tendetai = tendetai;
            this.Chuyennganh = chuyennganh;
            this.Congnghe = congnghe;
            this.Chucnang = chucnang;
            this.Ngonngu = ngonngu;
            this.Gvhd = gvhd;
            this.Masv1 = masv1;
            this.Masv2 = masv2;
            this.Yeucau = yeucau;
            this.Trangthai = trangthai;
            this.Tiendo = tiendo;
        }

        public string GetTendetai()
        {
            return this.Tendetai;
        }

        public string GetChuyennganh()
        {
            return this.Chuyennganh;
        }

        public string GetCongnghe()
        {
            return this.Congnghe;
        }

        public string GetChucnang()
        {
            return this.Chucnang;
        }

        public string GetNgonngu()
        {
            return this.Ngonngu;
        }

        public string GetGvhd()
        {
            return this.Gvhd;
        }

        public string GetMasv1()
        {
            return this.Masv1;
        }

        public string GetMasv2()
        {
            return this.Masv2;
        }

        public string GetYeucau()
        {
            return this.Yeucau;
        }

        public string GetTrangthai()
        {
            return this.Trangthai;
        }

        public int GetTiendo()
        {
            return this.Tiendo;
        }


    }

}
