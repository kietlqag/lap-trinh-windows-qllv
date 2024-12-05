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
    public partial class FFollow : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao();
        string ms;
        public FFollow(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void FFollow_Load(object sender, EventArgs e)
        {
            DataTable dataTable = gvdao.TienDo(ms);
            foreach (DataRow row in dataTable.Rows)
            {
                UCTienDo uctiendo = new UCTienDo();
                uctiendo.LabelClicked += Uctiendo_LabelClicked;
                uctiendo.madetai = row["madetai"].ToString();
                uctiendo.setTenDeTai(row["tendetai"].ToString());
                uctiendo.SetProgressValue(Convert.ToDouble(row["tiendo"]));
                uctiendo.setTienDo(row["tiendo"].ToString() + '%');

                flowLayoutPanel1.Controls.Add(uctiendo);
            }
        }

        private void Uctiendo_LabelClicked(object sender, EventArgs e)
        {
            UCTienDo uctiendo = sender as UCTienDo;
            FDetailsProcess fDetailsProcess = new FDetailsProcess(uctiendo.madetai, ms);
            fDetailsProcess.ShowDialog();
        }
    }
}
