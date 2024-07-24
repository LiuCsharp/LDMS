using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraEditors.Repository;

namespace LDMS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*if (!System.Windows.Forms.SystemInformation.TerminalServerSession && Screen.AllScreens.Length > 1)
                DevExpress.XtraEditors.WindowsFormsSettings.SetPerMonitorDpiAware();
            else
                DevExpress.XtraEditors.WindowsFormsSettings.SetDPIAware();
            WindowsFormsSettings.TrackWindowsAppMode = DevExpress.Utils.DefaultBoolean.True;*/



            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");//皮肤主题  
            string[] filetypes = ConfigurationManager.AppSettings["FileType"].Split(",");
            string outputDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 设定文件路径和名称
            string filePath = Path.Combine(outputDirectory, "Temp");

            if (!Directory.Exists(filePath)) 
            {
               Directory.CreateDirectory(filePath);
            }

            for (int i = 0; i < filetypes.Length; i++)
            {
                filePath = Path.Combine(outputDirectory, "Temp\\Temp" + filetypes[i]);
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    File.SetAttributes(filePath, FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.System);
                }
            }
                                     
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmNavigation());
            FrmNavigation frmmain = new FrmNavigation();

            if (frmmain.ShowDialog() == DialogResult.OK)
            {
                //frmmain.Close();
                FrmMain form1 = new FrmMain(frmmain.path);
                form1.ShowDialog();
            }
        }
    }
}
