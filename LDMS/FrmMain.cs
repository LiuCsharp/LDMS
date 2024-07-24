using CopyTxtInfo.Dto;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraLayout.Customization.Templates;
using LDMS.Dto;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDMS
{
    public partial class FrmMain : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        public string path { set; get; }

        FrmFolder frmFolder = new FrmFolder();
        public FrmMain()
        {
            InitializeComponent();
        }

        public FrmMain(string _path)
        {
            InitializeComponent();
            this.path = _path;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmFolder folder = new FrmFolder(path);
            frmFolder = folder;
            folder.MdiParent = this;
            folder.Parent = FrmFolder;
            folder.Show();
            frmlist.Add("FrmFolder");

            this.navigationPane1.SelectedPage= FrmFolder;
            /*FrmEmail frmEmail = new FrmEmail();
            frmEmail.MdiParent= this;
            frmEmail.Parent = backstageViewClientControl2;
            frmEmail.Show*/
            barButtonItem1.SearchTags = "未登录";
        }

        Form frm = new Form();
        List<string> frmlist = new List<string>();
        private void backstageViewControl1_SelectedTabChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            
        }

        private void FrmFolder_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {

        }

        private void backstageViewTabItem3_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {

        }

        private void backstageViewClientControl1_Load(object sender, EventArgs e)
        {

        }

        private void FrmEmail_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                //int count = frmFolder.fileList.Count;

                // FrmFolder frmFolder = new FrmFolder();

                string path = "Jsons\\Control.json";
                if (File.Exists(path))
                {

                    bool flag = frmFolder.controlDto.Any(x => x.Path == this.path);
                    if (!flag)
                    {
                        frmFolder.controlDto.Add(
                            new ControlDto
                            {
                                Path = this.path,
                                TreeViewDto = frmFolder.fileList,
                                TabPageDto = frmFolder.tabPaged,
                                ShortcutsControl = frmFolder.shortcutsControls,
                                FileDto = frmFolder.fileDtos
                                //ImageList=frmFolder.imageList

                            });
                    }
                    else
                    {
                        var c = frmFolder.controlDto.Where(x => x.Path == this.path).FirstOrDefault();
                        c.Path = this.path;
                        c.TreeViewDto = frmFolder.fileList;
                        c.TabPageDto = frmFolder.tabPaged;
                        c.ShortcutsControl = frmFolder.shortcutsControls;
                        c.FileDto = frmFolder.fileDtos;
                        //c.ImageList= frmFolder.imageList;
                    }
                }
                else
                {
                    frmFolder.controlDto.Add(
                            new ControlDto
                            {
                                Path = this.path,
                                TreeViewDto = frmFolder.fileList,
                                TabPageDto = frmFolder.tabPaged,
                                ShortcutsControl = frmFolder.shortcutsControls,
                                FileDto = frmFolder.fileDtos
                                //ImageList = frmFolder.imageList
                            });
                }

                string data = JsonConvert.SerializeObject(frmFolder.controlDto);
                File.WriteAllText(path, data);
                e.Cancel = false;
            }
            else if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        }

        private void navigationPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            string frmName = navigationPane1.SelectedPage.Name;

            bool b = frmlist.Any(x => x.Equals(frmName));
            if (!b)
            {
                frm = Comm.FormFactory.CreateForm(frmName, path);

                frm.MdiParent = this;
                if (frmName == "FrmFolder")
                {
                    frmFolder = (FrmFolder)frm;
                    frm.Parent = FrmFolder;
                    //frm.Dock = DockStyle.Fill;
                    //frm.Size = new Size(backstageViewClientControl1.Size.Width,backstageViewClientControl1.Height);
                }
                else if (frmName == "FrmNone")
                {
                    frm.Parent = FrmEmail;
                    //frm.Dock = DockStyle.Fill;
                }

                frmlist.Add(frmName);
                frm.Show();
            }

        }
    }
}