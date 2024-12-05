namespace PRPR
{
    partial class FMarkSV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customTextbox1 = new ControlLibrary.CustomTextbox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.customTextbox1);
            this.groupBox1.Font = new System.Drawing.Font("Sitka Small", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(233, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Xem điểm";
            // 
            // customTextbox1
            // 
            this.customTextbox1.BackColor = System.Drawing.Color.LavenderBlush;
            this.customTextbox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.customTextbox1.BottomBorderColor = System.Drawing.Color.DeepPink;
            this.customTextbox1.ButtomBorderFocusColor = System.Drawing.Color.Blue;
            this.customTextbox1.Enabled = false;
            this.customTextbox1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextbox1.ForeColor = System.Drawing.Color.Red;
            this.customTextbox1.Location = new System.Drawing.Point(29, 60);
            this.customTextbox1.Name = "customTextbox1";
            this.customTextbox1.ReadOnly = true;
            this.customTextbox1.Size = new System.Drawing.Size(481, 38);
            this.customTextbox1.TabIndex = 1;
            this.customTextbox1.Text = "12";
            this.customTextbox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FMarkSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(960, 563);
            this.Controls.Add(this.groupBox1);
            this.Name = "FMarkSV";
            this.Text = "FMarkSV";
            this.Load += new System.EventHandler(this.FMarkSV_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ControlLibrary.CustomTextbox customTextbox1;
    }
}