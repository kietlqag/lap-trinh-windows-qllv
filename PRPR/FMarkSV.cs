using ControlLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRPR
{
    public partial class FMarkSV : Form
    {
        CSinhVienDao svdao = new CSinhVienDao();
        string ms;
        public FMarkSV(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void FMarkSV_Load(object sender, EventArgs e)
        {
            DataTable dataTable = svdao.XemDiem(ms);
            if (dataTable.Rows.Count > 0)
            {
                customTextbox1.Text = dataTable.Rows[0]["diem"].ToString();
            }
        }
    }
}
