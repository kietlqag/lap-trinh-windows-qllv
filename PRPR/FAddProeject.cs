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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRPR
{
    public partial class FAddProeject : Form
    {
        CGiangVienDao gvdao = new CGiangVienDao(); 
        string ms;
        public FAddProeject(string ms)
        {
            InitializeComponent();
            this.ms = ms;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CDeTai detai = new CDeTai(textBox1.Text, comboBox1.Text, comboBox3.Text, textBox4.Text, comboBox2.Text, ms, null, null, textBox7.Text, "Chưa duyệt", 0);
            gvdao.ThemDeTai(detai);
            this.Close();
        }
    }
}
