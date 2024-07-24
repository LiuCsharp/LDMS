using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Tile;
using LDMS.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.Email;
using DevExpress.Utils.Helpers;
using DevExpress.Utils.CommonDialogs;
using DevExpress.XtraGrid.Columns;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;
using System.Net.Http.Json;
using System.IO;
using Newtonsoft.Json;
using CopyTxtInfo.Dto;
using DevExpress.CodeParser.JavaScript;
using DevExpress.Office.Utils;
using DevExpress.Data.Helpers;
using LDMS.Properties;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.TableLayout;
using DevExpress.CodeParser;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.Office.Interop.Word;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.XtraBars.Customization;
using DevExpress.DataAccess.Native.Data;
using DevExpress.XtraVerticalGrid.Native;
using DevExpress.Utils;
using LDMS.Comm;



namespace LDMS
{
    public partial class FrmNavigation : DevExpress.XtraEditors.XtraForm
    {
        public FrmNavigation()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.tileView1.DoubleClick += TileView1_DoubleClick;
            this.comboBoxEdit1.EditValueChanged += ComboBoxEdit1_EditValueChanged;
            this.comboBoxEdit1.ButtonClick += ComboBoxEdit1_ButtonClick;
            this.comboBoxEdit1.Properties.ContextButtonClick += Properties_ContextButtonClick;
        }
        public string path { get; set; }
        public BindingList<BindFile> dataTables = new BindingList<BindFile>();
        public List<string> search = new List<string>();
        public List<FileList> fileLists = new List<FileList>();

        private void FrmNavigation_Load(object sender, EventArgs e)
        {
            tileView1.OptionsTiles.GroupTextPadding = new Padding(-10, 0, 0, 10);
            BindTileView();
            BindComBox();
        }

        public void BindComBox()
        {
            search = UPDFile<string>.UPDFiles("Jsons\\Searce.json").ToList();
            foreach (string json in search)
            {
                comboBoxEdit1.Properties.Items.Add(json);
            }
        }

        public void BindTileView()
        {

            fileLists = UPDFile<FileList>.UPDFiles("Jsons\\FileList.json").OrderByDescending(x => x.FileTime).ToList();
            if (fileLists == null) { fileLists = new List<FileList>(); }

            foreach (var file in fileLists)
            {
                BindFile fileList = new BindFile();
                fileList.FileName = file.FileName;
                fileList.FilePath = file.FilePath;
                fileList.FileTime = file.FileTime;
                fileList.FileImage = GetImage(file.FilePath);
                fileList.FileDate = GetDay(file.FileDate);
                dataTables.Add(fileList);
            }
            this.gridControl1.DataSource = dataTables;
        }

        private void Properties_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            string value = e.DataItem.ToString();
            string selectedValue = comboBoxEdit1.SelectedText;
            if (value == selectedValue)
            {
                comboBoxEdit1.Text = "";
                comboBoxEdit1.Properties.Buttons.FirstOrDefault(x => x.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear).Visible = false;
                comboBoxEdit1.Properties.ContextImageOptions.Image = Resources.find_16x16;

            }
            comboBoxEdit1.Properties.Items.Remove(value);
            search.Remove(value);
            UPDFile<string>.UPDFile_A("Jsons\\Searce.json", search);

        }

        private void ComboBoxEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
            {
                string value = comboBoxEdit1.Text;
                if (value != "")
                {
                    bool flag = search.Contains(value);
                    if (!flag)
                    {
                        search.Add(value);
                        UPDFile<string>.UPDFile_A("Jsons\\Searce.json", search);
                        comboBoxEdit1.Properties.Items.Add(value);
                    }
                }
                comboBoxEdit1.Text = "";
                e.Button.Visible = false;
                //comboBoxEdit1.Properties.ShowDropDown= ShowDropDown.Never;

            }

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Down)
            {
                //comboBoxEdit1.Properties.ShowDropDown = ShowDropDown.DoubleClick;
                //comboBoxEdit1.Properties.Buttons[0].PerformClick();
            }
        }

        private void ComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            EditorButton button = comboBoxEdit1.Properties.Buttons.FirstOrDefault(x => x.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear);
            string value = comboBoxEdit1.Text;
            if (value != "")
            {
                var bindFiles = dataTables.Where(x => x.FilePath.Contains(value) || x.FileName.Contains(value)).OrderByDescending(e => e.FileTime);
                this.gridControl1.DataSource = bindFiles;
                comboBoxEdit1.Properties.ContextImageOptions.Image = null;
                button.Visible = true;

            }
            else
            {
                this.gridControl1.DataSource = dataTables;
                comboBoxEdit1.Properties.ContextImageOptions.Image = Resources.find_16x16;
                button.Visible = false;
            }
        }

        private void TileView1_DoubleClick(object sender, EventArgs e)
        {
            path = this.tileView1.GetFocusedRowCellValue(this.FilePath).ToString();
            var lists = fileLists.Where(x => x.FilePath == path).FirstOrDefault();
            if (fileLists.Contains(lists))
            {
                lists.FileTime = DateTime.Now;
                lists.FileDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            UPDFile<FileList>.UPDFile_A("Jsons\\FileList.json", fileLists);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string GetDay(string date)
        {
            string day = "";
            TimeSpan span = DateTime.Now - Convert.ToDateTime(date);

            if (span.TotalDays > 30)
            {
                decimal days = span.Days / 30;
                days = Math.Ceiling(days);
                day = days.ToString() + "月前";
            }
            else if (span.TotalDays > 21)
            {
                day = "3周前";
            }
            else if (span.TotalDays > 14)
            {
                day = "2周前";
            }
            else if (span.TotalDays > 7)
            {
                day = "1周前";
            }
            else if (span.TotalDays > 1)
            {
                day = string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                day = "今天";
            }

            return day;

        }

        protected Image GetImage(string fullName)
        {

            return FileSystemHelper.GetImage(fullName, IconSizeType.ExtraLarge, new Size(32, 32));
        }

        void OnDelete(object sender, TileViewHtmlElementMouseEventArgs e)
        {
            int index = tileView1.GetFocusedDataSourceRowIndex();//获取数据行的索引值，从0开始
            string path = tileView1.GetRowCellValue(index, "FilePath").ToString();//获取选中行的单元格的值 
            tileView1.DeleteRow(0);
            FileList fileList = fileLists.Find(x => x.FilePath == path);
            fileLists.Remove(fileList);
            UPDFile<FileList>.UPDFile_A("Jsons\\FileList.json", fileLists);
            //this.gridControl1.DataSource = fileLists;
        }

        private void tileBarItem1_ItemDoubleClick(object sender, TileItemEventArgs e)
        {

        }

        public FileList AddFile(string path)
        {
            FileList file = new FileList();
            string[] myPath = path.Split('\\');
            foreach (string s in myPath)
            {
                file.FileName = s;
            }

            file.FilePath = path;
            file.FileTime = DateTime.Now;
            file.FileDate = DateTime.Now.ToString("yyyy-MM-dd");
            return file;
        }

        private void tileBarItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            FolderBrowserDialog myFolderBrowserDialog = new FolderBrowserDialog();
            myFolderBrowserDialog.ShowNewFolderButton = false;
            myFolderBrowserDialog.ShowDialog();

            if (myFolderBrowserDialog.SelectedPath != "")
            {
                string path = "Jsons\\FileList.json";
                if (!File.Exists(path))
                {
                    List<FileList> fileList = new List<FileList> { AddFile(myFolderBrowserDialog.SelectedPath) };
                    UPDFile<FileList>.UPDFile_A(path, fileList);
                }
                else
                {
                    //path = myFolderBrowserDialog.SelectedPath;
                    var lists = fileLists.Where(x => x.FilePath == myFolderBrowserDialog.SelectedPath).FirstOrDefault();
                    if (fileLists.Contains(lists))
                    {
                        lists.FileTime = DateTime.Now;
                        lists.FileDate = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        fileLists.Add(AddFile(myFolderBrowserDialog.SelectedPath));
                    }
                    UPDFile<FileList>.UPDFile_A(path, fileLists);
                }

                this.path = myFolderBrowserDialog.SelectedPath;
                /*FrmMain frmMain = new FrmMain(path);
                frmMain.Show();
                this.Hide();*/
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void FrmNavigation_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*try
            {
                 System.Windows.Forms.Application.Exit();
            }
            catch
            {
            }*/
        }

        /*private void comboBoxEdit1_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            Graphics graphics = e.Graphics;
        }*/
    }
}