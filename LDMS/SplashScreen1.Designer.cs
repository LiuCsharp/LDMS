namespace LDMS
{
    partial class SplashScreen1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen1));
            progressBarControl = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            labelStatus = new DevExpress.XtraEditors.LabelControl();
            peImage = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)progressBarControl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)peImage.Properties).BeginInit();
            SuspendLayout();
            // 
            // progressBarControl
            // 
            progressBarControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            progressBarControl.EditValue = 0;
            progressBarControl.Location = new System.Drawing.Point(32, 321);
            progressBarControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            progressBarControl.Name = "progressBarControl";
            progressBarControl.Size = new System.Drawing.Size(536, 17);
            progressBarControl.TabIndex = 5;
            // 
            // labelStatus
            // 
            labelStatus.Location = new System.Drawing.Point(32, 298);
            labelStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 1);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new System.Drawing.Size(95, 18);
            labelStatus.TabIndex = 7;
            labelStatus.Text = "文件加载中....";
            // 
            // peImage
            // 
            peImage.Dock = System.Windows.Forms.DockStyle.Top;
            peImage.EditValue = resources.GetObject("peImage.EditValue");
            peImage.Location = new System.Drawing.Point(1, 1);
            peImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            peImage.Name = "peImage";
            peImage.Properties.AllowFocused = false;
            peImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            peImage.Properties.Appearance.Options.UseBackColor = true;
            peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peImage.Properties.ShowMenu = false;
            peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            peImage.Properties.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            peImage.Size = new System.Drawing.Size(598, 277);
            peImage.TabIndex = 9;
            // 
            // SplashScreen1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(600, 443);
            Controls.Add(peImage);
            Controls.Add(labelStatus);
            Controls.Add(progressBarControl);
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "SplashScreen1";
            Padding = new System.Windows.Forms.Padding(1);
            Text = "SplashScreen1";
            ((System.ComponentModel.ISupportInitialize)progressBarControl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)peImage.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.LabelControl labelStatus;
        private DevExpress.XtraEditors.PictureEdit peImage;
    }
}
