namespace LDMS
{
    partial class FrmMain
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
            components = new System.ComponentModel.Container();
            formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            toolbarFormControl1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormControl();
            toolbarFormManager1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(components);
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            navigationPane1 = new DevExpress.XtraBars.Navigation.NavigationPane();
            FrmFolder = new DevExpress.XtraBars.Navigation.NavigationPage();
            FrmEmail = new DevExpress.XtraBars.Navigation.NavigationPage();
            FrmMyFolder = new DevExpress.XtraBars.Navigation.NavigationPage();
            FrmSettings = new DevExpress.XtraBars.Navigation.NavigationPage();
            ((System.ComponentModel.ISupportInitialize)toolbarFormControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toolbarFormManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)navigationPane1).BeginInit();
            navigationPane1.SuspendLayout();
            SuspendLayout();
            // 
            // toolbarFormControl1
            // 
            toolbarFormControl1.Location = new System.Drawing.Point(0, 0);
            toolbarFormControl1.Manager = toolbarFormManager1;
            toolbarFormControl1.Name = "toolbarFormControl1";
            toolbarFormControl1.Size = new System.Drawing.Size(1321, 39);
            toolbarFormControl1.TabIndex = 27;
            toolbarFormControl1.TabStop = false;
            toolbarFormControl1.Text = "文件管理系统";
            toolbarFormControl1.TitleItemLinks.Add(barButtonItem2);
            toolbarFormControl1.TitleItemLinks.Add(barButtonItem3);
            toolbarFormControl1.TitleItemLinks.Add(barButtonItem1);
            toolbarFormControl1.ToolbarForm = this;
            // 
            // toolbarFormManager1
            // 
            toolbarFormManager1.DockControls.Add(barDockControlTop);
            toolbarFormManager1.DockControls.Add(barDockControlBottom);
            toolbarFormManager1.DockControls.Add(barDockControlLeft);
            toolbarFormManager1.DockControls.Add(barDockControlRight);
            toolbarFormManager1.Form = this;
            toolbarFormManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { barButtonItem1, barButtonItem2, barButtonItem3, barSubItem1, barButtonItem4, barSubItem2, barSubItem3 });
            toolbarFormManager1.MaxItemId = 12;
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            barDockControlTop.Location = new System.Drawing.Point(0, 39);
            barDockControlTop.Manager = toolbarFormManager1;
            barDockControlTop.Size = new System.Drawing.Size(1321, 0);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            barDockControlBottom.Location = new System.Drawing.Point(0, 651);
            barDockControlBottom.Manager = toolbarFormManager1;
            barDockControlBottom.Size = new System.Drawing.Size(1321, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            barDockControlLeft.Location = new System.Drawing.Point(0, 39);
            barDockControlLeft.Manager = toolbarFormManager1;
            barDockControlLeft.Size = new System.Drawing.Size(0, 612);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            barDockControlRight.Location = new System.Drawing.Point(1321, 39);
            barDockControlRight.Manager = toolbarFormManager1;
            barDockControlRight.Size = new System.Drawing.Size(0, 612);
            // 
            // barButtonItem1
            // 
            barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            barButtonItem1.Caption = "未登录";
            barButtonItem1.Id = 5;
            barButtonItem1.ImageOptions.SvgImage = Properties.Resources.bo_customer4;
            barButtonItem1.Name = "barButtonItem1";
            barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem2
            // 
            barButtonItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            barButtonItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            barButtonItem2.Caption = "天气";
            barButtonItem2.Id = 6;
            barButtonItem2.ImageOptions.SvgImage = Properties.Resources.weather_sunny1;
            barButtonItem2.Name = "barButtonItem2";
            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem3
            // 
            barButtonItem3.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            barButtonItem3.Caption = "问题反馈";
            barButtonItem3.Id = 7;
            barButtonItem3.ImageOptions.SvgImage = Properties.Resources.about2;
            barButtonItem3.Name = "barButtonItem3";
            barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barSubItem1
            // 
            barSubItem1.Caption = "文件(F)";
            barSubItem1.Id = 8;
            barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(barSubItem2) });
            barSubItem1.Name = "barSubItem1";
            // 
            // barSubItem2
            // 
            barSubItem2.Caption = "分享到";
            barSubItem2.Id = 10;
            barSubItem2.Name = "barSubItem2";
            // 
            // barButtonItem4
            // 
            barButtonItem4.Caption = "分享";
            barButtonItem4.Id = 9;
            barButtonItem4.Name = "barButtonItem4";
            // 
            // barSubItem3
            // 
            barSubItem3.Caption = "barSubItem3";
            barSubItem3.Id = 11;
            barSubItem3.Name = "barSubItem3";
            // 
            // navigationPane1
            // 
            navigationPane1.Controls.Add(FrmFolder);
            navigationPane1.Controls.Add(FrmEmail);
            navigationPane1.Controls.Add(FrmMyFolder);
            navigationPane1.Controls.Add(FrmSettings);
            navigationPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            navigationPane1.Location = new System.Drawing.Point(0, 39);
            navigationPane1.LookAndFeel.SkinName = "WXI";
            navigationPane1.LookAndFeel.UseDefaultLookAndFeel = false;
            navigationPane1.Name = "navigationPane1";
            navigationPane1.PageProperties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.Image;
            navigationPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { FrmFolder, FrmEmail, FrmMyFolder, FrmSettings });
            navigationPane1.RegularSize = new System.Drawing.Size(1321, 612);
            navigationPane1.SelectedPage = FrmFolder;
            navigationPane1.Size = new System.Drawing.Size(1321, 612);
            navigationPane1.TabIndex = 37;
            navigationPane1.Text = "navigationPane1";
            navigationPane1.SelectedPageChanged += navigationPane1_SelectedPageChanged;
            // 
            // FrmFolder
            // 
            FrmFolder.BackgroundPadding = new System.Windows.Forms.Padding(0);
            FrmFolder.Caption = "文件管理";
            FrmFolder.ImageOptions.SvgImage = Properties.Resources.FolderOpen;
            FrmFolder.Name = "FrmFolder";
            FrmFolder.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            FrmFolder.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            FrmFolder.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.Image;
            FrmFolder.Size = new System.Drawing.Size(1234, 575);
            // 
            // FrmEmail
            // 
            FrmEmail.Caption = "邮件中心";
            FrmEmail.ImageOptions.SvgImage = Properties.Resources.Message;
            FrmEmail.Name = "FrmEmail";
            FrmEmail.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            FrmEmail.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            FrmEmail.Size = new System.Drawing.Size(1321, 612);
            // 
            // FrmMyFolder
            // 
            FrmMyFolder.BackgroundPadding = new System.Windows.Forms.Padding(0);
            FrmMyFolder.Caption = "我的文件";
            FrmMyFolder.ImageOptions.SvgImage = Properties.Resources.FavoriteStar;
            FrmMyFolder.Name = "FrmMyFolder";
            FrmMyFolder.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            FrmMyFolder.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            FrmMyFolder.Size = new System.Drawing.Size(1321, 612);
            // 
            // FrmSettings
            // 
            FrmSettings.BackgroundPadding = new System.Windows.Forms.Padding(0);
            FrmSettings.Caption = "系统设置";
            FrmSettings.ImageOptions.SvgImage = Properties.Resources.Setting;
            FrmSettings.Name = "FrmSettings";
            FrmSettings.Size = new System.Drawing.Size(1321, 612);
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1321, 651);
            Controls.Add(navigationPane1);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Controls.Add(toolbarFormControl1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            HelpButton = true;
            IconOptions.SvgImage = Properties.Resources.Ethernet;
            IsMdiContainer = true;
            Name = "FrmMain";
            ToolbarFormControl = toolbarFormControl1;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            ((System.ComponentModel.ISupportInitialize)toolbarFormControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)toolbarFormManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)navigationPane1).EndInit();
            navigationPane1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormControl toolbarFormControl1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormManager toolbarFormManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.Navigation.NavigationPane navigationPane1;
        private DevExpress.XtraBars.Navigation.NavigationPage FrmFolder;
        private DevExpress.XtraBars.Navigation.NavigationPage FrmEmail;
        private DevExpress.XtraBars.Navigation.NavigationPage FrmMyFolder;
        private DevExpress.XtraBars.Navigation.NavigationPage FrmSettings;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
    }
}