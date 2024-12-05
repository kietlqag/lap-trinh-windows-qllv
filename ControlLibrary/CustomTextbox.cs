using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ControlLibrary
{
    public partial class CustomTextbox : TextBox
    {
        private Color _bottomBorderColor = Color.Black;
        private Color _onFocusColor = Color.Blue;
        public CustomTextbox()
        {
            BorderStyle = BorderStyle.None;
            AutoSize = false;

            Controls.Add(new Label
            {
                Height = 2,
                Dock = DockStyle.Bottom,
                BackColor = _bottomBorderColor
            });
            InitializeComponent();
        }

        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        public Color BottomBorderColor
        {
            get { return _bottomBorderColor; }
            set
            {
                _bottomBorderColor = value;
                Controls[0].BackColor = _bottomBorderColor;
            }
        }

        public Color ButtomBorderFocusColor
        {
            get { return _onFocusColor;  }
            set { _onFocusColor = value; }
        }
    }
}
