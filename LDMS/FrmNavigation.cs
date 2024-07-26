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
using System.Text.RegularExpressions;



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
        public List<AddFilePath> addFiles = new List<AddFilePath>();


        private void FrmNavigation_Load(object sender, EventArgs e)
        {
            tileView1.OptionsTiles.GroupTextPadding = new Padding(-10, 0, 0, 10);
            panelControl3.Visible = false;
            panelControl2.Visible = false;
            BindTileView();
            BindComBox();
            TbFileName.Properties.AppearanceFocused.BackColor = Color.FromArgb(248, 248, 248);
            CmbFilePath.Properties.AppearanceFocused.BackColor = Color.FromArgb(248, 248, 248);
            comboBoxEdit1.Properties.AppearanceFocused.BackColor = Color.FromArgb(248, 248, 248);
            /*TbFileName.Properties.Appearance.BackColor = Color.FromArgb(230, 230, 230);
            CmbFilePath.Properties.Appearance.BackColor = Color.FromArgb(230, 230, 230);*/

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

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }

        private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            panelControl1.Visible = false;
            panelControl2.Visible = true;
            panelControl3.Visible = true;


            int count = CmbFilePath.Properties.Items.Count;
            if (count == 0)
            {
                BindAddFileComBox();
            }


            TbFileName.Focus();
            TbFileName.BorderStyle = BorderStyles.Simple;
        }


        public void GetUniqueFileName(string path)
        {
            string uniqueFileName = "新建文件夹";
            int counter = 1;
            if (path == "") return;

            // 检查文件名是否重复
            while (Directory.Exists(path + "\\" + uniqueFileName))
            {
                // 通过正则表达式从文件名中提取数字后缀
                var match = Regex.Match("新建文件夹", @"^(.*?)(-\d+)?\.([^.]+)$");
                if (match.Success)
                {
                    // 如果已经有数字后缀，就增加它
                    if (match.Groups[2].Success)
                    {
                        uniqueFileName = $"{match.Groups[1].Value}({counter++}).{match.Groups[3].Value}";

                    }
                    else
                    {
                        // 添加第一个数字后缀
                        uniqueFileName = $"{match.Groups[1].Value}({counter++}).{match.Groups[3].Value}";
                    }
                }
                else
                {
                    // 如果没有找到匹配，就添加一个新的数字后缀
                    uniqueFileName = $"新建文件夹({counter++})";
                }
            }

            TbFileName.Text = uniqueFileName;

            labelControl8.Text = $"文件夹 将在\"{path}\\\" 中创建";
        }

        public void BindAddFileComBox()
        {
            addFiles = UPDFile<AddFilePath>.UPDFiles("Jsons\\AddFilePath.json");
            foreach (var path in addFiles)
            {
                CmbFilePath.Properties.Items.Add(path.FilePath);
                if (path.LastOpen)
                {
                    CmbFilePath.SelectedItem = path.FilePath;
                    GetUniqueFileName(path.FilePath);
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            panelControl1.Visible = true;
            panelControl2.Visible = false;
            panelControl3.Visible = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string fileName = TbFileName.Text;
            if (fileName == "")
            {
                MessageBox.Show("请输入文件夹名称！");
                TbFileName.Focus();
                return;
            }
            string filePath = CmbFilePath.Text;
            if (filePath == "")
            {
                MessageBox.Show("请选择文件夹路径！");
                CmbFilePath.Focus();
                return;
            }

            if (File.Exists(filePath + "\\" + fileName))
            {
                MessageBox.Show("该文件夹在当前路径已存在！");
                TbFileName.Focus();
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("当前路径不存在！");
                CmbFilePath.Focus();
                return;
            }

            Directory.CreateDirectory(filePath + "\\" + fileName);

            foreach (var file in addFiles)
            {
                file.LastOpen = false;
            }

            var b = addFiles.FirstOrDefault(x => x.FilePath == filePath);
            if (b != null)
            {
                b.OpenNumber++;
                b.LastOpen = true;
            }
            else
            {
                addFiles.Add(new AddFilePath { FilePath = filePath, OpenNumber = 1, LastOpen = true });
            }

            UPDFile<AddFilePath>.UPDFile_A("Jsons\\AddFilePath.json", addFiles);

            this.path = filePath + "\\" + fileName;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog myFolderBrowserDialog = new FolderBrowserDialog();
            myFolderBrowserDialog.ShowNewFolderButton = false;
            myFolderBrowserDialog.ShowDialog();

            if (myFolderBrowserDialog.SelectedPath != "")
            {
                CmbFilePath.Text = myFolderBrowserDialog.SelectedPath;
                GetUniqueFileName(myFolderBrowserDialog.SelectedPath);
                CmbFilePath.Select();
                CmbFilePath.BorderStyle = BorderStyles.Simple;
            }
        }

        private void CmbFilePath_EditValueChanged(object sender, EventArgs e)
        {
            string path = CmbFilePath.Text;
            labelControl8.Text = $"文件夹 将在\"{path}\\\" 中创建";
        }

        private void TbFileName_Leave(object sender, EventArgs e)
        {
            TbFileName.BorderStyle = BorderStyles.NoBorder;

        }

        private void TbFileName_Click(object sender, EventArgs e)
        {
            TbFileName.BorderStyle = BorderStyles.Simple;
        }

        private void CmbFilePath_Leave(object sender, EventArgs e)
        {
            CmbFilePath.BorderStyle = BorderStyles.NoBorder;
        }

        private void CmbFilePath_Click(object sender, EventArgs e)
        {
            CmbFilePath.BorderStyle = BorderStyles.Simple;
        }

        private void CmbFilePath_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void comboBoxEdit1_Leave(object sender, EventArgs e)
        {
            comboBoxEdit1.BorderStyle = BorderStyles.NoBorder;
        }

        private void comboBoxEdit1_Click(object sender, EventArgs e)
        {
            comboBoxEdit1.BorderStyle = BorderStyles.Simple;
        }
    }
}