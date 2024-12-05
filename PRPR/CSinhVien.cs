using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRPR
{
    internal class CSinhVien
    {
        private string ms;
        private string hoten;
        private string ngaysinh;
        private string noisinh;
        private string dantoc;
        private string quequan;
        private string khoahoc;
        private string hinhthuc;
        private string lopkhoa;
        private string tinhtrang;
        private string sdt;
        private string email;
        private string noio;
        private string sdtnt;
        private string matkhau;
        public CSinhVien(string ms, string hoten, string ngaysinh, string noisinh, string dantoc, string quequan, string khoahoc, string hinhthuc, string lopkhoa, string tinhtrang, string sdt, string email, string noio, string sdtnt, string matkhau)
        {
            this.ms = ms;
            this.hoten = hoten;
            this.ngaysinh = ngaysinh;
            this.noisinh = noisinh;
            this.dantoc = dantoc;
            this.quequan = quequan;
            this.khoahoc = khoahoc;
            this.hinhthuc = hinhthuc;
            this.lopkhoa = lopkhoa;
            this.tinhtrang = tinhtrang;
            this.sdt = sdt;
            this.email = email;
            this.noio = noio;
            this.sdtnt = sdtnt;
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
        public string KhoaHoc
        {
            get { return this.khoahoc; }
        }
        public string HinhThuc
        {
            get { return this.hinhthuc; }
        }
        public string LopKhoa
        {
            get { return this.lopkhoa; }
        }
        public string TinhTrang
        {
            get { return this.tinhtrang; }
        }
        public string SDT
        {
            get { return this.sdt; }
        }
        public string Email
        {
            get { return this.email; }
        }
        public string NoiO
        {
            get { return this.noio; }
        }
        public string SDTNT
        {
            get { return this.sdtnt; }
        }

        public string MatKhau
        {
            get { return this.matkhau; }
        }

    }
}
