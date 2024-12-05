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
    public partial class UCTienDo : UserControl
    {
        public event EventHandler LabelClicked;
        public string madetai { get; set; }

        public UCTienDo()
        {
            InitializeComponent();
            label1.Click += label1_Click;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            LabelClicked?.Invoke(this, EventArgs.Empty);
        }

        public void setTenDeTai(string text)
        {
            label1.Text = text;
        }

        public void SetProgressValue(double value)
        {
            if (value < progressBar1.Minimum)
            {
                progressBar1.Value = progressBar1.Minimum;
            }
            else if (value > progressBar1.Maximum)
            {
                progressBar1.Value = progressBar1.Maximum;
            }
            else
            {
                progressBar1.Value = (int)value;
            }
        }


        public void setTienDo(string text)
        {
            label3.Text = text;
        }

        private void UCTienDo_Load(object sender, EventArgs e)
        {

        }
    }
}
