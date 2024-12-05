using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRPR
{
    internal class CThongBao
    {
        string tieude {get; set;}
        string noidung { get; set; }
        string msnguoigui { get; set; }
        string msnguoinhan { get; set; }
        string thoidiem { get; set; }
        string matb { get; set; }

        public CThongBao(string tieude, string noidung, string msnguoigui, string msnguoinhan, string thoidiem, string matb)
        {
            this.tieude = tieude;
            this.noidung = noidung;
            this.msnguoigui = msnguoigui;
            this.msnguoinhan = msnguoinhan;
            this.thoidiem = thoidiem;
            this.matb = matb;
        }

        public string Tieude
        {
            get { return this.tieude; }
        }

        public string Noidung
        {
            get { return this.noidung; }
        }

        public string Msnguoigui
        {
            get { return this.msnguoigui; }
        }

        public string Msnguoinhan
        {
            get { return this.msnguoinhan; }
        }

        public string Thoidiem
        {
            get { return this.thoidiem; }
        }

        public string Matb
        {
            get { return this.matb; }
        }
    }
}
