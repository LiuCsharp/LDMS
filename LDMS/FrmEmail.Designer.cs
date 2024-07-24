namespace LDMS
{
    partial class FrmEmail
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
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            SuspendLayout();
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new System.Drawing.Point(12, 12);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(118, 36);
            simpleButton1.TabIndex = 0;
            simpleButton1.Text = "simpleButton1";
            // 
            // FrmEmail
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1246, 575);
            Controls.Add(simpleButton1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmEmail";
            Text = "FrmEmail";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}