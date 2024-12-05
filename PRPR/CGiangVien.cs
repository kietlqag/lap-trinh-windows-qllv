using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRPR
{
    internal class CGiangVien
    {
        private string ms;
        private string hoten;
        private string ngaysinh;
        private string noisinh;
        private string dantoc;
        private string quequan;
        private string khoa;
        private string loai;
        private string hocvi;
        private string chucvu;
        private string sdt;
        private string mail;
        private string noio;
        private string sdtngth;
        private string matkhau;
        public CGiangVien(string ms, string hoten, string ngaysinh, string noisinh, string dantoc, string quequan, string khoa, string loai, string hocvi, string chucvu, string sdt, string mail, string noio, string sdtngth, string matkhau)
        {
            this.ms = ms;
            this.hoten = hoten;
            this.ngaysinh = ngaysinh;
            this.noisinh = noisinh;
            this.dantoc = dantoc;
            this.quequan = quequan;
            this.khoa = khoa;
            this.loai = loai;
            this.hocvi = hocvi;
            this.chucvu = chucvu;
            this.sdt = sdt;
            this.mail = mail;
            this.noio = noio;
            this.sdtngth = sdtngth;
            this.matkhau = matkhau;
        }
        public string MS
        {
            get { return this.ms; }
        }
        public string HoTen
        {
            get { return this.hoten; }
        }
        public string NgaySinh
        {
            get { return this.ngaysinh; }
        }
        public string NoiSinh
        {
            get { return this.noisinh; }
        }
        public string DanToc
        {
            get { return this.dantoc; }
        }
        public string QueQuan
        {
            get { return this.quequan; }
        }
        public string Khoa
        {
            get { return this.khoa; }
        }
        public string Loai
        { get { return this.loai; } }
        public string Hocvi
        { get { return this.hocvi; } }
        public string Chucvu
        { get { return this.chucvu; } }
        public string SDT
        {
            get { return this.sdt; }
        }
        public string Mail
        { get { return this.mail; } }
        public string NoiO
        { get { return this.noio; } }
        public string SDTNGTH
        { get { return this.sdtngth; } }
        public string MatKhau
        { get { return this.matkhau; } }
    }
}

