using System;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCDeTai : UserControl
    {
        public event EventHandler LinkLabelClicked;
        public event EventHandler LinkLabelClicked2;
        public UCDeTai()
        {
            InitializeComponent();
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
        }

        public void SetMaDeTai(string text)
        {
            lblMaDeTai.Text = text;
        }

        public string GetMaDeTai()
        {
            return lblMaDeTai.Text;
        }

        public void SetTenDeTai(string text)
        {
            lblTenDeTai.Text = text;
        }

        public void SetChuyenNganh(string text)
        {
            lblChuyenNganh.Text = text;
        }

        public void SetGVHD(string text)
        {
            linkGVHD.Text = text;
        }

        public void SetTrangThai(string text)
        {
            label2.Text = text;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnLinkedLabelClicked(EventArgs.Empty);
        }

        protected virtual void OnLinkedLabelClicked(EventArgs e)
        {
            LinkLabelClicked?.Invoke(this, e);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnLinkLabelClicked2(EventArgs.Empty);
        }

        protected virtual void OnLinkLabelClicked2(EventArgs e)
        {
            LinkLabelClicked2?.Invoke(this, e);
        }

    }
}