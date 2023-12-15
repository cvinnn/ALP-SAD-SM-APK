namespace ALP_SAD_SM_APK
{
    partial class simpantrx
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
            this.btn_nntisj = new Guna.UI2.WinForms.Guna2Button();
            this.btn_tmbh = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // btn_nntisj
            // 
            this.btn_nntisj.BorderRadius = 2;
            this.btn_nntisj.BorderThickness = 1;
            this.btn_nntisj.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_nntisj.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_nntisj.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_nntisj.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_nntisj.FillColor = System.Drawing.Color.White;
            this.btn_nntisj.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_nntisj.ForeColor = System.Drawing.Color.Black;
            this.btn_nntisj.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_nntisj.Location = new System.Drawing.Point(9, 142);
            this.btn_nntisj.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_nntisj.Name = "btn_nntisj";
            this.btn_nntisj.Size = new System.Drawing.Size(109, 19);
            this.btn_nntisj.TabIndex = 2;
            this.btn_nntisj.Text = "Nanti Saja";
            this.btn_nntisj.Click += new System.EventHandler(this.btn_nntisj_Click);
            // 
            // btn_tmbh
            // 
            this.btn_tmbh.BorderRadius = 2;
            this.btn_tmbh.BorderThickness = 1;
            this.btn_tmbh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_tmbh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_tmbh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_tmbh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_tmbh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btn_tmbh.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tmbh.ForeColor = System.Drawing.Color.White;
            this.btn_tmbh.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btn_tmbh.Location = new System.Drawing.Point(122, 142);
            this.btn_tmbh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_tmbh.Name = "btn_tmbh";
            this.btn_tmbh.Size = new System.Drawing.Size(109, 19);
            this.btn_tmbh.TabIndex = 3;
            this.btn_tmbh.Text = "Tambah Baru";
            this.btn_tmbh.Click += new System.EventHandler(this.btn_tmbh_Click);
            // 
            // simpantrx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ALP_SAD_SM_APK.Properties.Resources.Group_40;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(240, 171);
            this.ControlBox = false;
            this.Controls.Add(this.btn_tmbh);
            this.Controls.Add(this.btn_nntisj);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "simpantrx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "simpantrx";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btn_nntisj;
        private Guna.UI2.WinForms.Guna2Button btn_tmbh;
    }
}