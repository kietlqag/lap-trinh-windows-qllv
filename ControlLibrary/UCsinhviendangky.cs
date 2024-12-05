using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCsinhviendangky : UserControl
    {
        public UCsinhviendangky()
        {
            InitializeComponent();
        }

        public void SetMSSV(string text)
        {
            lblMSSV.Text = text;
        }
        public void SetHoTen(string text)
        {
            lblHoTen.Text = text;
        }

        public void SetLop(string text)
        {
            lblLop.Text = text;
        }
    }
}
