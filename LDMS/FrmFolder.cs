using CopyTxtInfo.Dto;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.WinExplorer;
using DevExpress.XtraTreeList.Nodes;
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
using DevExpress.Utils.Helpers;
using CopyTxtInfo.Comm;
using DevExpress.XtraTab;
using DevExpress.XtraSplashScreen;
using System.Diagnostics;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraBars.Docking2010.Views.Widget;
using System.Text.RegularExpressions;
using DevExpress.XtraRichEdit.API.Native;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using DevExpress.XtraPrinting.Native;
using Word = Microsoft.Office.Interop.Word;
using DevExpress.XtraTreeList;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;
using System.Threading;
using DevExpress.XtraRichEdit;
using DevExpress.CodeParser;
using Excel = Microsoft.Office.Interop.Excel;
using System.Configuration;
using DevExpress.Mvvm.Native;
using DevExpress.Map.Kml.Model;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using DevExpress.XtraLayout.Customization.Templates;
using DevExpress.ClipboardSource.SpreadsheetML;
using Windows.Storage;
using DevExpress.Utils.VisualEffects;
using Microsoft.Extensions.Configuration;
using DevExpress.XtraScheduler;
using LDMS.Properties;
using DevExpress.Data.ExpressionEditor;
using LDMS.Dto;
using DevExpress.DataAccess.Wizard.Presenters;


namespace LDMS
{
    public partial class FrmFolder : DevExpress.XtraEditors.XtraForm
    {
        private delegate void UpdateTreeDelegate(TreeNode node);
        private static string CurrentFolderPath;
        public List<FileDto> fileDtos { get; set; }

        public List<TabPageDto> tabPaged { get; set; }
        public List<TreeViewDto> fileList { get; set; }
        public List<ShortcutsControl> shortcutsControls { get; set; }

        List<ShortcutsImages> shortcutsImages = new List<ShortcutsImages>();

        private List<ImageDto> imageDtos = new List<ImageDto>();
        public List<ControlDto> controlDto { get; set; }
        int a = 0;
        int b = 0;
        public ImageList imageList { get; set; }
        //ImageList image = new ImageList();
        private Size ImageSize;
        bool isS = false;
        public Dictionary<string, XtraTabPage> Pairs = new Dictionary<string, XtraTabPage>();
        bool IsSel = false;
        bool valueflag = true;
        int imageid = 0;
        bool changeFlag = true;
        //TreeListNode rootNode = new TreeListNode();
        public FrmFolder()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public FrmFolder(string path)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            CurrentFolderPath = path;
            tabPaged = new List<TabPageDto>();
            fileList = new List<TreeViewDto>();
            shortcutsControls = new List<ShortcutsControl>();
            controlDto = new List<ControlDto>();
            fileDtos = new List<FileDto>();
            imageList = new ImageList();
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            //Dictionary<string,List<>>= JsonConvert.DeserializeObject<List<T>>(jsons);
        }

        private void FrmFolder_Load(object sender, EventArgs e)
        {
            UpdateOther();
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            BindDgv();
            LoadControls();
            string flag = System.Configuration.ConfigurationManager.AppSettings["ShowProperty"];
            if (flag == "true")
            {
                groupControl2.Visible = true;
                splitter1.Visible = true;
            }
            else 
            {
                groupControl2.Visible = false;
                splitter1.Visible = false;
            }
           
            SplashScreenManager.CloseForm(true);
        }

        public void BindDgv()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("PropertyCode");
            dt.Columns.Add("PropertyName");

            for (int i = 0; i < 4; i++)
            {
                PropertyCode p = (PropertyCode)i;
                DataRow dataRow = dt.NewRow();
                dataRow["PropertyCode"] = p.ToString();
                dt.Rows.Add(dataRow);
            }
            this.gridControl2.DataSource = dt;

        }

        public void UpdateOther()
        {
            xtraTabControl1.AllowDrop = true;
            xtraTabControl1.KeyUp += XtraTabControl1_KeyUp;
            xtraTabControl1.SelectedPageChanged += XtraTabControl1_SelectedPageChanged;
            treeList1.KeyFieldName = "Id";
            treeList1.ParentFieldName = "ParentId";

            treeList1.OptionsBehavior.Editable = false;
            treeList1.RowHeight = 20;

            treeList1.MouseDoubleClick -= treeList1_MouseDoubleClick_InOne;
            treeList1.MouseDoubleClick += treeList1_MouseDoubleClick_InOne;
            treeList1.ForceInitialize();
            treeList1.SelectImageList = imageList;
            
            treeList1.Appearance.FocusedCell.BackColor = Color.FromArgb(205, 230, 247); // 选中行背景色
            winExplorerView1.ItemClick += WinExplorerView1_ItemClick;
            winExplorerView1.ItemDoubleClick += WinExplorerView1_ItemDoubleClick;
            winExplorerView1.OptionsView.Style = WinExplorerViewStyle.Medium;
            AddShortcut.ItemClick += AddShortcut_ItemClick;
            DelShortcut.ItemClick += DelShortcut_ItemClick;
            AddFolder.ItemClick += AddFolder_ItemClick;
            AddExcel.ItemClick += AddExcel_ItemClick;
            AddWord.ItemClick += AddWord_ItemClick;
            AddDOCX.ItemClick += AddDOCX_ItemClick;
            AddXLSX.ItemClick += AddXLSX_ItemClick;
            AddTxt.ItemClick += AddTxt_ItemClick;
            DelFile.ItemClick += DelFile_ItemClick;
            CloseOneTab.ItemClick += CloseOneTab_ItemClick;
            CloseLeftTab.ItemClick += CloseLeftTab_ItemClick;
            CloseRightTab.ItemClick += CloseRightTab_ItemClick;
            CloseNoOneTab.ItemClick += CloseNoOneTab_ItemClick;
            CloseAllTab.ItemClick += CloseAllTab_ItemClick;
            FileProperty.ItemClick += FileProperty_ItemClick;
            ImageSize = new Size(16, 16);
            
            string outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //Bitmap image = GetDefaultAppIcon(outputDirectory + "Temp\\Temp.txt");
            AddFolder.ImageOptions.LargeImage = GetImage(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            AddTxt.ImageOptions.LargeImage = GetImage(outputDirectory + "Temp\\Temp.txt");
            AddExcel.ImageOptions.LargeImage = GetImage(outputDirectory + "Temp\\Temp.xls");
            AddXLSX.ImageOptions.LargeImage = GetImage(outputDirectory + "Temp\\Temp.xlsx");
            AddWord.ImageOptions.LargeImage = GetImage(outputDirectory + "Temp\\Temp.doc");
            AddDOCX.ImageOptions.LargeImage = GetImage(outputDirectory + "Temp\\Temp.docx");
            SendWeChat.ImageOptions.LargeImage = GetImage(Environment.GetEnvironmentVariable("WeChat"));

            groupControl2.AppearanceCaption.BorderColor = Color.FromArgb(230, 230, 230);
        }



        private void CloseAllTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            changeFlag = false;
            int count = xtraTabControl1.TabPages.Count;
            for (int i = 0; i < count; i++)
            {
                CloseTab(xtraTabControl1.TabPages[1]);
            }
            changeFlag = true;
        }

        private void CloseNoOneTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage xtraTab = xtraTabControl1.SelectedTabPage;
            string path = Pairs.FirstOrDefault(x => x.Value == xtraTab).Key;
            int count = xtraTabControl1.TabPages.Count;
            changeFlag = false;
            for (int i = count - 1; i > 0; i--)
            {
                XtraTabPage page = xtraTabControl1.TabPages[i];
                if (page == xtraTab) continue;
                CloseTab(page);
            }
            changeFlag = true;
        }

        private void CloseRightTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            int index = xtraTabControl1.SelectedTabPageIndex;
            int count = xtraTabControl1.TabPages.Count - index - 1;
            changeFlag = false;
            for (int i = 0; i < count; i++)
            {

                CloseTab(xtraTabControl1.TabPages[index + 1]);
            }
            changeFlag = true;
        }

        private void CloseLeftTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            changeFlag = false;
            int index = xtraTabControl1.SelectedTabPageIndex;
            for (int i = 1; i < index; i++)
            {
                CloseTab(xtraTabControl1.TabPages[1]);
            }
            changeFlag = true;
        }

        private void CloseOneTab_ItemClick(object sender, ItemClickEventArgs e)
        {
            CloseTab(xtraTabControl1.SelectedTabPage);
        }

        private void XtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            /*xtraTabControl1.Focus();
            XtraTabPage xtraTab = xtraTabControl1.SelectedTabPage;
            foreach (Control control in xtraTab.Controls)
            {
                control.Focus();
                control.Select();
            }*/
        }

        protected void MemoEdit_KeyUp(object sender, KeyEventArgs e)
        {
            //int count = tabPaged.Count;

            if ((Control.ModifierKeys & Keys.Control) != 0 && e.KeyCode == Keys.S)
            {
                MemoEdit memo = sender as MemoEdit;
                XtraTabPage xtraTabPage = memo.Parent as XtraTabPage;
                /*TabPageDto tabPage = tabPaged.FirstOrDefault(x => x.TabIndex == xtraTabControl1.SelectedTabPageIndex);
                FileDto fileDto = fileDtos.FirstOrDefault(e => e.FileID == tabPage.TabID);

                if (fileDto.FileType == "txt")
                {
                    File.WriteAllText(fileDto.FilePath, memo.EditValue.ToString());
                }*/
            }
        }

        private void XtraTabControl1_KeyUp(object sender, KeyEventArgs e)
        {

        }



        List<TreeListNode> nodeIds = new List<TreeListNode>();
        public void DelTreeListNode(TreeListNode node)
        {
            foreach (TreeListNode nodeId in node.Nodes.ToList())
            {
                TreeListNode childNode = nodeId;
                if (childNode.Nodes.Count > 0)
                {
                    DelTreeListNode(childNode);
                }

                int id1 = childNode.Id;
                FileDto td1 = fileDtos.Find(x => x.FileID == id1);
                TreeViewDto viewDto1 = fileList.Find(e => e.Id == id1);
                if (td1.FileType == "Folder")
                {
                    Directory.Delete(td1.FilePath);
                }
                else
                {
                    File.Delete(td1.FilePath);
                }
                node.Nodes.Remove(childNode);
                fileDtos.Remove(td1);
                fileList.Remove(viewDto1);
                imageList.Images.RemoveAt(id1);
                int i = 0;
                foreach (var item in fileList)
                {
                    item.Id = i;
                    i++;
                }

                int j = 0;
                foreach (var item in fileDtos)
                {
                    item.FileID = j;
                    j++;
                }
            }
        }

        private void DelFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;

            int id = node.Id;
            FileDto td = fileDtos.FirstOrDefault(x => x.FileID == id);
            TreeViewDto viewDto = fileList.Find(e => e.Id == id);

            DelTreeListNode(node);

            if (td.FileType == "Folder")
            {
                Directory.Delete(td.FilePath);
            }
            else
            {
                File.Delete(td.FilePath);
            }

            treeList1.Nodes.Remove(node);
            //imageList.Images.RemoveAt(id);

            fileDtos.Remove(td);
            fileList.Remove(viewDto);

            int i = 0;
            // imageList.Images.Clear();
            foreach (var item in fileList)
            {
                item.Id = i;
                i++;
            }

            int j = 0;
            foreach (var item in fileDtos)
            {
                item.FileID = j;
                j++;
                //imageList.Images.Add(GetImage(item.FilePath));

            }

            //SetTreeViewData();
        }

        private void AddXLSX_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;

            int id = node.Id;
            FileDto trd = fileDtos.Find(x => x.FileID == id);
            AddTreeListNode(trd, node, "xlsx", "office");
        }

        private void AddDOCX_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;

            int id = node.Id;
            FileDto trd = fileDtos.Find(x => x.FileID == id);
            AddTreeListNode(trd, node, "docx", "office");
        }

        private void AddTxt_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;

            int id = node.Id;
            FileDto trd = fileDtos.Find(x => x.FileID == id);
            AddTreeListNode(trd, node, "txt", "text");
        }

        private void AddWord_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;

            int id = node.Id;
            FileDto trd = fileDtos.Find(x => x.FileID == id);
            AddTreeListNode(trd, node, "doc", "office");
        }

        private void AddExcel_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;

            int id = node.Id;
            FileDto trd = fileDtos.Find(x => x.FileID == id);
            AddTreeListNode(trd, node, "xls", "office");
        }

        private void AddFolder_ItemClick(object sender, ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;

            int id = node.Id;
            FileDto trd = fileDtos.Find(x => x.FileID == id);
            AddTreeListNode(trd, node, "Folder", "Folder");
        }

        public void AddTreeListNode(FileDto trd, TreeListNode node, string filetype, string FileParentType)
        {
            TreeListNode node1 = new TreeListNode();

            if (trd.FileType != "Folder")
            {
                node1 = node.ParentNode;
            }
            else
            {
                node1 = node;
            }

            string name = GetTreeListName(filetype, node1);
            TreeListNode tNode = treeList1.AppendNode(name, node1);

            int id = node1.Id;
            string path = fileDtos.First(x => x.FileID == id).FilePath;
            path = path + "\\" + name;
            bool flag = imageDtos.Any(x => x.ImageType == filetype);
            int maxid = imageDtos.Max(x => x.ImageID);
            if (!flag)
            {
                imageList.Images.Add(GetImage(path));
                maxid = maxid + 1;
                imageDtos.Add(new ImageDto { ImageID = maxid, ImageType = filetype });
            }

            long fileSize = 0;
            if (filetype == "Folder")
            {
                fileSize = Convert.ToInt64(Directory.GetFiles(path).Sum(t => new System.IO.FileInfo(t).Length));
            }
            else
            {
                fileSize = new System.IO.FileInfo(path).Length;

            }
            tNode.ImageIndex = maxid;
            tNode.SelectImageIndex = maxid;

            fileDtos.Add(new FileDto() { FilePath = path, FileID = tNode.Id, FileName = name, FileType = filetype, FileParentType = FileParentType, FileSize = fileSize });
            fileList.Add(new TreeViewDto() { Id = tNode.Id, ParentId = tNode.ParentNode.Id, TName = name });
            tNode.SetValue("TName", name);
            //treeList1.RefreshDataSource();
        }

        public string GetTreeListName(string filetype, TreeListNode node)
        {
            string name = "";
            if (filetype == "Folder")
            {
                name = "新建文件夹";
            }

            if (filetype == "xls")
            {
                name = "新建 Microsoft Excel 工作表.xls";
            }

            if (filetype == "xlsx")
            {
                name = "新建 Microsoft Excel 工作表.xlsx";
            }

            if (filetype == "txt")
            {
                name = "新建 文本文档.txt";
            }

            if (filetype == "doc")
            {
                name = "新建 Microsoft Word 文档.doc";
            }

            if (filetype == "docx")
            {
                name = "新建 Microsoft Word 文档.docx";
            }

            int id = node.Id;
            string path = fileDtos.First(x => x.FileID == id).FilePath;
            //path = path + "\\" + name;
            name = GetUniqueFileName(path, name, filetype);
            /*path = path + "\\" + name;
            Directory.CreateDirectory(path);*/
            return name;
        }

        public static bool CheckFile(string folderPath, string fileName, string filetype)
        {
            bool isFile = false;
            if (filetype == "Folder")
            {
                isFile = Directory.Exists(folderPath + "\\" + fileName);
            }
            else
            {
                isFile = File.Exists(folderPath + "\\" + fileName);
            }

            return isFile;
        }

        public static string GetUniqueFileName(string folderPath, string fileName, string filetype)
        {
            string uniqueFileName = fileName;
            int counter = 1;


            // 检查文件名是否重复
            while (CheckFile(folderPath, uniqueFileName, filetype))
            {
                // 通过正则表达式从文件名中提取数字后缀
                var match = Regex.Match(fileName, @"^(.*?)(-\d+)?\.([^.]+)$");
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
                    uniqueFileName = $"{fileName}({counter++})";
                }
            }

            string file = folderPath + "\\" + uniqueFileName;
            var fileInfo = new System.IO.FileInfo(file);
            if (filetype == "Folder")
            {
                Directory.CreateDirectory(file);
            }
            else if (filetype == "xls" || filetype == "xlsx")
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 设置许可
                using (var package = new ExcelPackage())
                {
                    // 在包中添加一个新的工作表
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1").Workbook.Worksheets.Add("Sheet2");
                    // 将此工作表保存到文件系统                 
                    package.SaveAs(fileInfo);
                }
            }
            else if (filetype == "doc" || filetype == "docx")
            {
                // 创建Word应用程序实例
                Word.Application wordApp = new Word.Application();
                //wordApp.Visible = true; // 使Word可见

                // 创建新的Word文档
                Word.Document wordDoc = wordApp.Documents.Add();

                // 添加文本到文档
                //wordDoc.Paragraphs.Last.Range.Text = "这是一个Word文档的示例文本。";

                // 以下代码可以保存文档到一个具体路径

                wordDoc.SaveAs2(file);
                wordDoc.Close();
                wordApp.Quit();

                // 释放对象
                wordDoc = null;
                wordApp = null;
            }
            else if (filetype == "txt")
            {
                File.WriteAllText(file, "");
            }

            return uniqueFileName;
        }

        public void ClearList()
        {
            fileDtos.Clear();
            fileList.Clear();
            treeList1.Nodes.Clear();
            imageList.Images.Clear();
            imageList = new ImageList();
            treeList1.SelectImageList = imageList;
        }

        private void DelShortcut_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] rows = winExplorerView1.GetSelectedRows();
            if (rows.Length == 0) return;
            for (int i = rows.Max(); i <= rows.Length; i--)
            {
                if (i == rows[0] - 1) break;
                winExplorerView1.DeleteRow(i);
            }
        }

        public void LoadControls()
        {
            string path = "Jsons\\Control.json";
            if (File.Exists(path))
            {
                /* ConfigurationBuilder builder = new ConfigurationBuilder();
                 builder.AddJsonFile("Jsons\\Control.json", optional: true, reloadOnChange: true);
                 IConfigurationRoot root = builder.Build();

                 //root.GetSection("").Value;
                 foreach (var con in root.GetChildren()) 
                 {

                 }*/
                string jsons = File.ReadAllText(path);
                controlDto = JsonConvert.DeserializeObject<List<ControlDto>>(jsons);
            };

            bool flag = controlDto.Any(x => x.Path == CurrentFolderPath);

            if (!flag)
            {
                LoadFolderMethod();
            }
            else
            {
                var b = controlDto.Where(x => x.Path == CurrentFolderPath);
                foreach (var item in b)
                {

                    shortcutsControls = item.ShortcutsControl;
                    foreach (var sm in shortcutsControls)
                    {
                        ShortcutsImages shortcutsImage = new ShortcutsImages();
                        shortcutsImage.ShortcutsID = sm.ShortcutsID;
                        shortcutsImage.ShortcutsPath = sm.ShortcutsPath;
                        shortcutsImage.ShortcutsName = sm.ShortcutsName;
                        shortcutsImage.ShortcutsText = sm.ShortcutsText;
                        System.Drawing.Icon icon = SystemIcons.WinLogo;

                        icon = System.Drawing.Icon.ExtractAssociatedIcon(sm.ShortcutsPath);
                        Image largeIcon = icon.ToBitmap();
                        shortcutsImage.ShortcutsImage = largeIcon;
                        shortcutsImages.Add(shortcutsImage);
                    }

                    gridControl1.DataSource = shortcutsImages;
                    winExplorerView1.RefreshData();
                    BeginInvoke(new MethodInvoker(winExplorerView1.ClearSelection));

                    fileList = item.TreeViewDto;
                    fileDtos = item.FileDto;
                    int imageid1 = 0;
                    foreach (var fileDto in fileDtos)
                    {
                        bool flag1 = imageDtos.Any(x => x.ImageType == fileDto.FileType);
                        if (!flag1)
                        {
                            imageList.Images.Add(GetImage(fileDto.FilePath));
                            imageDtos.Add(new ImageDto { ImageID = imageid1, ImageType = fileDto.FileType });
                            imageid1++;
                        }

                    }

                    imageList.Images.Add(Resources.opendoc_16x16);
                    imageid = imageList.Images.Count - 1;

                    if (fileList.Count > 0)
                    {
                        treeList1.DataSource = fileList;
                        SetTreeViewData();
                    }
                    tabPaged = item.TabPageDto;
                    AddTabAsync();
                }

                //treeList1.Select();
                TabPageDto pageDto = tabPaged.FirstOrDefault(x => x.IsLocation == true);
                if (pageDto != null)
                {
                    TreeListNode pFocusNode2 = this.treeList1.FindNodeByFieldValue("TName", pageDto.TabName);

                    treeList1.FocusedNode = pFocusNode2;

                }
                IsSel = true;
                BindDgv_A();
            }

        }

        public void AddTabAsync()
        {
            AddControl addControl = new AddControl();
            int index = 1;

            foreach (var tabPage in tabPaged)
            {

                int Sum = xtraTabControl1.TabPages.Count;
                Sum = Sum + 1 - 1;
                FileDto fileDto = fileDtos.FirstOrDefault(x => x.FileID == tabPage.TabID);
                XtraTabPage tab1 = xtraTabControl1.TabPages.Add(tabPage.TabText);
                tab1.Name = tabPage.TabID.ToString();
                xtraTabControl1.TabPages[Sum].ImageOptions.Image = GetImage(fileDto.FilePath);

                tab1.Tooltip = tabPage.TabName;
                if (tabPage.IsLocation)
                {
                    xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[index];
                    tab1.Controls.Add(addControl.AddControls(fileDto.FilePath, fileDto.FileParentType, fileDto.FileType));
                }

                index++;
                Pairs.Add(fileDto.FilePath, tab1);

            }

        }

        public void BindTreeView()
        {

            FolderBrowserDialog myFolderBrowserDialog = new FolderBrowserDialog();
            myFolderBrowserDialog.ShowNewFolderButton = false;
            myFolderBrowserDialog.ShowDialog();

            if (myFolderBrowserDialog.SelectedPath != "")
            {
                if (fileDtos.Count > 0)
                {
                    FileDto fileDto = fileDtos.Find(x => x.FilePath == myFolderBrowserDialog.SelectedPath);
                    if (fileDtos.Contains(fileDto))
                    {
                        XtraMessageBox.Show("该文件已存在！");
                        return;
                    }
                }

                ClearList();
                CurrentFolderPath = myFolderBrowserDialog.SelectedPath;
            }
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            LoadControls();
            SplashScreenManager.CloseForm(true);
        }

        long fileSize = 0;
        /// <summary>
        /// 把文件添加到fileList
        /// </summary>
        /// <param name="path"></param>
        internal void LoadFolderFileList(string path, TreeListNode listNode)
        {
            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            ImageSize = new Size(16, 16);
            for (int i = 0; i < dirs.Length; i++)
            {
                string[] info = new string[4];
                DirectoryInfo di = new DirectoryInfo(dirs[i]);

                //imageList.Images.Add(GetImage(di.FullName));
                TreeListNode pNode = treeList1.AppendNode(di.Name, listNode);
                fileSize = Convert.ToInt64(Directory.GetFiles(di.FullName).Sum(t => new System.IO.FileInfo(t).Length));
                pNode.ImageIndex = 0;
                pNode.SelectImageIndex = 0;
                fileDtos.Add(new FileDto() { FilePath = di.FullName, FileID = pNode.Id, FileName = di.Name, FileType = "Folder", FileParentType = "Folder", FileSize = fileSize });
                fileList.Add(new TreeViewDto() { Id = pNode.Id, ParentId = pNode.ParentNode.Id, TName = di.Name });
                //pNode.SetValue(pNode.Id, di.Name);
                try
                {
                    if (di.GetDirectories().Length > 0 || di.GetFiles().Length > 0)
                    {
                        b = a;
                        a = a + 1;

                        LoadFolderFileList(di.FullName, pNode);

                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception)
                {

                    continue;
                }
            }

            for (int j = 0; j < files.Length; j++)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(files[j]);

                string fileType = fi.Extension.Replace(".", "");
                string fileParentType = string.Empty;

                if (fileType.Substring(0, 3) == "doc" || fileType.Substring(0, 3) == "xls" || fileType.Substring(0, 3) == "pdf")
                {
                    fileParentType = FileParentType.office.ToString();
                }

                if (fileType.Substring(0, 3) == "txt" || fileType.Substring(0, 3) == "C")
                {
                    fileParentType = FileParentType.text.ToString();
                }

                if (fileType.Substring(0, 3) == "jpg")
                {
                    fileParentType = FileParentType.image.ToString();
                }

                bool flag = imageDtos.Any(x => x.ImageType == fileType);
                int maxid = imageDtos.Max(x => x.ImageID);
                if (!flag)
                {
                    imageList.Images.Add(FileSystemHelper.GetImage(fi.FullName, IconSizeType.Small, ImageSize));
                    maxid = maxid + 1;
                    imageDtos.Add(new ImageDto { ImageID = maxid, ImageType = fileType });
                }

                //FileType file = (FileType)Enum.Parse(typeof(FileType), fileType.Substring(0, 3));
                TreeListNode hNode = treeList1.AppendNode(fi.Name, listNode);
                hNode.ImageIndex = maxid;
                hNode.SelectImageIndex = maxid;
                fileSize = fi.Length;
                fileDtos.Add(new FileDto() { FilePath = fi.FullName, FileID = hNode.Id, FileName = fi.Name, FileType = fileType, FileParentType = fileParentType, FileSize = fileSize });
                fileList.Add(new TreeViewDto() { Id = hNode.Id, ParentId = hNode.ParentNode.Id, TName = fi.Name });
                a = a + 1;
            }
        }


        /// <summary>
        /// 绑定treelist
        /// </summary>
        private void LoadFolderMethod()
        {
            treeList1.Nodes.Clear();
            //UpdateTreeDelegate loadTreeView = new UpdateTreeDelegate(AddToTreeView);

            string[] myPath = CurrentFolderPath.Split('\\');

            string path = "";
            FileDto filedto = new FileDto();
            foreach (string s in myPath)
            {
                path = s + "\\";
                filedto.FileName = path.Replace("\\", "");

            }
            imageList.Images.Add(GetImage(CurrentFolderPath));

            TreeListNode rootNode = treeList1.Nodes.Add(filedto.FileName);
            rootNode.ImageIndex = rootNode.Id;
            rootNode.SelectImageIndex = rootNode.Id;
            filedto.FileID = rootNode.Id;
            filedto.FilePath = CurrentFolderPath;
            filedto.FileType = "Folder";
            filedto.FileParentType = "Folder";
            //filedto.FileImage = FileSystemHelper.GetImage(CurrentFolderPath, IconSizeType.Small, ImageSize);
            fileDtos.Add(filedto);
            fileList.Add(new TreeViewDto() { Id = rootNode.Id, ParentId = 0, TName = filedto.FileName, IsExpand = true });
            imageDtos.Add(new ImageDto { ImageID = 0, ImageType = "Folder" });
            b = a;
            a = rootNode.Id;

            LoadFolderFileList(CurrentFolderPath, rootNode);
            imageList.Images.Add(Resources.opendoc_16x16);
            imageid = imageList.Images.Count - 1;
            treeList1.Nodes[0].Expand();
            BindDgv_A();
            //controlDto.Add(new ControlDto { Path = CurrentFolderPath, TreeViewDto = fileList });
        }
        int c = 0;
        public void SetTreeViewData()
        {
            foreach (TreeListNode root in treeList1.Nodes)
            {
                int id = root.Id;
                TreeViewDto treeView = fileList.FirstOrDefault(x => x.Id == id);
                if (treeView.IsExpand)
                {
                    root.Expand();
                    root.ImageIndex = imageid;
                    root.SelectImageIndex = imageid;
                }
                else
                {
                    root.ImageIndex = 0;
                    root.SelectImageIndex = 0;
                }


                SetTreeViewImage(root);
            }
        }

        private void TreeList1_MouseLeave(object sender, EventArgs e)
        {
            treeList1.OptionsBehavior.Editable = false;
        }

        public void SetTreeViewImage(TreeListNode treeList)
        {
            foreach (TreeListNode list1 in treeList.Nodes)
            {
                TreeViewDto treeView = fileList.FirstOrDefault(x => x.Id == list1.Id);
                if (treeView.IsExpand)
                {
                    list1.Expand();
                    list1.ImageIndex = imageid;
                    list1.SelectImageIndex = imageid;
                }
                else
                {
                    FileDto file = fileDtos.FirstOrDefault(x => x.FileID == list1.Id);
                    int id = imageDtos.FirstOrDefault(e => e.ImageType == file.FileType).ImageID;
                    list1.ImageIndex = id;
                    list1.SelectImageIndex = id;
                }

                TreeListNode treeList1 = list1;
                if (treeList1.Nodes.Count > 0)
                {
                    SetTreeViewImage(treeList1);
                }
                else
                {
                    continue;
                }
            }
        }

        protected Image GetImage(string fullName)
        {
            return FileSystemHelper.GetImage(fullName, IconSizeType.Small, new Size(20,20));
        }

        protected string StartupPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.Desktop); } }

        string _currentPath;

        private void AddShortcut_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<FileDto> listFiles = fileDtos.ToList();
            TreeListNode listNode = treeList1.FocusedNode;
            int id = listNode.Id;
            string treeViewName = listNode.GetValue("TName").ToString().Trim();
            FileDto fileDto = listFiles.Find(x => x.FileID == id);

            System.Drawing.Icon icon = SystemIcons.WinLogo;

            icon = System.Drawing.Icon.ExtractAssociatedIcon(fileDto.FilePath);
            Image largeIcon = icon.ToBitmap();
            shortcutsControls.Add(new ShortcutsControl()
            {
                ShortcutsID = id,
                ShortcutsPath = fileDto.FilePath,
                ShortcutsName = treeViewName,
                ShortcutsText = treeViewName
            });

            shortcutsImages.Add(new ShortcutsImages
            {
                ShortcutsID = id,
                ShortcutsPath = fileDto.FilePath,
                ShortcutsName = treeViewName,
                ShortcutsText = treeViewName,
                ShortcutsImage = largeIcon
            });
            _currentPath = StartupPath;
            gridControl1.DataSource = shortcutsImages;
            winExplorerView1.RefreshData();
            BeginInvoke(new MethodInvoker(winExplorerView1.ClearSelection));
        }

        private void treeList1_MouseDoubleClick_InOne(object sender, MouseEventArgs e)
        {
            TreeListNode listNode = treeList1.FocusedNode;

            int id = listNode.Id;
            string treeViewName = fileList.First(x => x.Id == id).TName;
            GetTreeList(treeViewName, id);
        }

        private void WinExplorerView1_ItemDoubleClick(object sender, WinExplorerViewItemDoubleClickEventArgs e)
        {
            string name = (string)winExplorerView1.GetFocusedRowCellValue("ShortcutsName");
            int id = (int)winExplorerView1.GetFocusedRowCellValue("ShortcutsID");
            GetTreeList(name, id);
        }

        private void WinExplorerView1_ItemClick(object sender, WinExplorerViewItemClickEventArgs e)
        {

        }

        private void barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {

            TreeListNode listNode = treeList1.FocusedNode;
            string treeViewName = "";
            string path = "";
            int id = 0;
            string type;
            bool b = fileList.Any(x => x.Id == listNode.Id);
            if (b)
            {
                //treeViewName = listNode.GetValue("Name").ToString().Trim();
                id = listNode.Id;
                treeViewName = fileList.FirstOrDefault(x => x.Id == id).TName;
                FileDto fileDto = fileDtos.Find(x => x.FileID == id);
                path = fileDto.FilePath;
                type = fileDto.FileType;
            }

            if (e.Item.Name == "OpenWin")
            {
                string directoryPath = System.IO.Path.GetDirectoryName(path);
                Process.Start("explorer.exe", directoryPath);
            }

            if (e.Item.Name == "PreView")
            {
                GetTreeList(treeViewName, id);
            }

            if (e.Item.Name == "OpenNew")
            {

                isS = false;
                BindTreeView();
                isS = true;
            }
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.popupMenu2.ShowPopup(Control.MousePosition);
                if (fileList.Count == 0)
                {

                    this.PreView.Enabled = false;
                    this.CopyPath.Enabled = false;
                    this.DelFile.Enabled = false;
                    this.Rename.Enabled = false;
                    this.OpenWin.Enabled = false;
                    this.Open.Enabled = false;
                    this.AddShortcut.Enabled = false;
                }
                else
                {
                    this.Open.Enabled = true;
                    this.OpenWin.Enabled = true;
                    this.AddShortcut.Enabled = true;
                    List<FileDto> listFiles = fileDtos.ToList();
                    TreeListNode listNode = treeList1.FocusedNode;

                    int id = listNode.Id;
                    FileDto files = listFiles.Find(x => x.FileID == id);
                    Open.ImageOptions.LargeImage = GetImage(files.FilePath);

                    string fileType = files.FileType;
                    ShortcutsControl shortcuts = shortcutsControls.Find(r => r.ShortcutsID == id);
                    XtraTabPage tabPage = xtraTabControl1.SelectedTabPage;
                    if (fileType == "" || fileType == "folder")
                    {
                        this.PreView.Enabled = false;
                        this.CopyPath.Enabled = false;
                        this.DelFile.Enabled = false;

                    }
                    else
                    {
                        this.PreView.Enabled = true;
                        this.CopyPath.Enabled = true;
                        this.DelFile.Enabled = true;
                        this.Rename.Enabled = true;
                        if (shortcutsControls.Contains(shortcuts) || tabPage.Text != "首页")
                        {
                            this.AddShortcut.Enabled = false;
                        }
                        else
                        {
                            this.AddShortcut.Enabled = true;
                        }
                    }

                }
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            int count = winExplorerView1.RowCount;
            if (e.Button == MouseButtons.Right && count > 0)
            {
                this.popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void GetTreeList(string treeViewName, int id)
        {
            List<FileDto> listFiles = fileDtos.ToList();
            if (!string.IsNullOrEmpty(treeViewName))
            {
                FileDto files = listFiles.Find(x => x.FileName == treeViewName && x.FileID == id);
                string filePath = files.FilePath;
                string fileType = files.FileType;
                string fileParentType = files.FileParentType;
                /* if (!File.Exists(filePath))
                 {
                     XtraMessageBox.Show("该文件不存在！");
                     return;
                 }*/
                if (fileType == "Folder" || !File.Exists(filePath)) return;
                SplashScreenManager.ShowForm(typeof(SplashScreen1));
                OpenTab(treeViewName, fileParentType, fileType, id, filePath);
                SplashScreenManager.CloseForm(true);
            }
        }

        public void UpdIsLocation()
        {
            int count = tabPaged.Count;
            if (count > 0)
            {
                var tab = tabPaged.Where(x => x.TabID > 0);
                foreach (var item in tab)
                {
                    item.IsLocation = false;
                }
            }

        }

        public bool CheckTabPageControl(XtraTabPage tabPage)
        {
            bool flag = false;
            int count = tabPage.Controls.Count;
            if (count > 0) flag = true;
            return flag;
        }

        public void OpenTab(string tab, string fileParentType, string filetype, int fileID, string filePath)
        {
            IsSel = false;
            AddControl addControl = new AddControl();
            int Sum = xtraTabControl1.TabPages.Count;
            Sum = Sum + 1 - 1;
            TabPageDto t = tabPaged.Find(x => x.TabID == fileID);
            FileDto f = fileDtos.Find(x => x.FileID == fileID);
            UpdIsLocation();
            if (!tabPaged.Contains(t))
            {
                XtraTabPage tab1 = xtraTabControl1.TabPages.Add(tab);
                xtraTabControl1.TabPages[Sum].ImageOptions.Image = GetImage(f.FilePath);
                tab1.Tooltip = tab;
                xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[Sum];
                Pairs.Add(f.FilePath, tab1);
                tab1.Name = fileID.ToString();
                try
                {
                    if (filetype == "txt")
                    {
                        FrmTxt frmTxt = new FrmTxt(filePath);
                        //frmTxt.MdiParent = this;
                        //
                        /*frmTxt.TopLevel = false;
                        frmTxt.Parent = tab1;*/

                        frmTxt.TopLevel = false;
                        tab1.Controls.Add(frmTxt);
                        frmTxt.Show();
                    }
                    else
                    {
                        tab1.Controls.Add(addControl.AddControls(filePath, fileParentType, filetype));
                    }
                    tabPaged.Add(new TabPageDto() { TabID = fileID, TabName = tab, TabText = tab, IsLocation = true, TabPath = filePath, TabIndex = xtraTabControl1.TabPages.Count - 1 });
                    return;

                }
                catch (IOException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }

                //COM.FormFactory.MessageEvent += CloseTab;


                //tab1.TabPageWidth = 100;


            }
            else
            {
                for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
                {
                    string Name = xtraTabControl1.TabPages[i].Name;

                    if (Name == fileID.ToString())
                    {
                        bool flag = CheckTabPageControl(xtraTabControl1.TabPages[i]);
                        TabPageDto tabPage = tabPaged.FirstOrDefault(x => x.TabID == fileID);
                        tabPage.IsLocation = true;
                        xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[i];
                        if (!flag)
                        {
                            xtraTabControl1.TabPages[i].Controls.Add(addControl.AddControls(filePath, fileParentType, filetype));
                        }

                        break;
                    }
                }
            }
            IsSel = true;
        }

        private void treeList1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                TreeListNode node = treeList1.FocusedNode;
                int a = node.Id;
                bool flag = tabPaged.Any(x => x.TabID == a);
                if (flag)
                {
                    MessageBox.Show("该文件被预览，不允许修改名称！");
                    return;
                }
                //treeList1.OptionsSelection.MultiSelect = true;

                treeList1.OptionsBehavior.Editable = true;

            }
        }

        public WinExplorerViewStyle ViewStyle { get { return this.winExplorerView1.OptionsView.Style; } }

        Size GetItemSize(WinExplorerViewStyle viewStyle)
        {
            switch (viewStyle)
            {
                case WinExplorerViewStyle.ExtraLarge: return new Size(256, 256);
                case WinExplorerViewStyle.Large: return new Size(96, 96);
                case WinExplorerViewStyle.Content: return new Size(32, 32);
                case WinExplorerViewStyle.Small: return new Size(16, 16);
                case WinExplorerViewStyle.Tiles:
                case WinExplorerViewStyle.Default:
                case WinExplorerViewStyle.List:
                case WinExplorerViewStyle.Medium: return new Size(48, 48);
                default: return new Size(96, 96);
            }
        }
        IconSizeType GetItemSizeType(WinExplorerViewStyle viewStyle)
        {
            switch (viewStyle)
            {
                case WinExplorerViewStyle.Large:
                case WinExplorerViewStyle.ExtraLarge: return IconSizeType.ExtraLarge;
                case WinExplorerViewStyle.List:
                case WinExplorerViewStyle.Small: return IconSizeType.Small;
                case WinExplorerViewStyle.Tiles:
                case WinExplorerViewStyle.Medium: return IconSizeType.Medium;
                case WinExplorerViewStyle.Content: return IconSizeType.Large;
                default: return IconSizeType.ExtraLarge;
            }
        }

        private void treeList1_MouseClick(object sender, MouseEventArgs e)
        {
            treeList1.OptionsBehavior.Editable = false;
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            treeList1.OptionsBehavior.Editable = false;
            BindDgv_A();


        }

        public void BindDgv_A()
        {
            int count = treeList1.Nodes.Count;
            if (count > 0)
            {
                int id = treeList1.FocusedNode.Id;

                FileDto file = fileDtos.FirstOrDefault(x => x.FileID == id);
                if (file != null)
                {
                    long fileSize = (long)Math.Round(file.FileSize / 1024.0);
                    this.gridView1.SetRowCellValue(0, PropertyName, file.FileName);
                    this.gridView1.SetRowCellValue(1, PropertyName, file.FilePath);
                    this.gridView1.SetRowCellValue(2, PropertyName, file.FileType);
                    this.gridView1.SetRowCellValue(3, PropertyName, fileSize + "KB");
                }

            }
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            XtraTabPage page = (e as ClosePageButtonEventArgs).Page as XtraTabPage;

            CloseTab(page);
        }

        /// <summary>
        /// 关闭页签
        /// </summary>
        /// <param name="TabName"></param>
        public void CloseTab(XtraTabPage page)
        {
            if (page.Text == "首页") return;
            xtraTabControl1.TabPages.Remove(page);

            string path = Pairs.FirstOrDefault(x => x.Value == page).Key;
            Pairs.Remove(path);

            FileDto fileDto = fileDtos.FirstOrDefault(x => x.FilePath == path);
            TabPageDto tabPageDto = tabPaged.FirstOrDefault(x => x.TabID == fileDto.FileID);
            tabPaged.Remove(tabPageDto);

            foreach (Control t in page.Controls)
            {
                if (t is System.Windows.Forms.Control)
                    t.Controls.Clear();
            }

            page.Dispose();

            xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[xtraTabControl1.TabPages.Count - 1];
            return;

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        int v_TabPage_X = 0;
        int v_TabPage_Y = 0;

        private void Tab_MouseDown(object sender, MouseEventArgs e)
        {
            this.v_TabPage_X = e.X;
            this.v_TabPage_Y = e.Y;
        }

        private void Tab_MouseUp(object sender, MouseEventArgs e)
        {
            this.v_TabPage_X = 0;
            this.v_TabPage_Y = 0;
        }

        private void Tab_MouseMove(object sender, MouseEventArgs e)
        {
            XtraTabControl xtraTabControl = (XtraTabControl)sender;
            bool flag = e.Button != MouseButtons.Left;
            if (!flag)
            {
                bool flag2 = Math.Abs(e.X - this.v_TabPage_X) <= 5;
                if (!flag2)
                {
                    XtraTabPage selectedTabPage = xtraTabControl.SelectedTabPage;
                    int num = xtraTabControl.TabPages.IndexOf(selectedTabPage);
                    xtraTabControl.DoDragDrop(selectedTabPage, DragDropEffects.Move);
                }
            }
        }

        private int Tab_getHoverTabIndex(XtraTabControl tc)
        {
            XtraTabHitInfo xtraTabHitInfo = tc.CalcHitInfo(tc.PointToClient(Cursor.Position));
            int result;
            for (int i = 0; i < tc.TabPages.Count; i++)
            {
                bool flag = xtraTabHitInfo.Page != null;
                if (flag)
                {
                    bool flag2 = xtraTabHitInfo.HitTest == XtraTabHitTest.PageHeader;
                    if (flag2)
                    {
                        bool flag3 = xtraTabHitInfo.Page == tc.TabPages[i];
                        if (flag3)
                        {
                            result = i;
                            return result;
                        }
                    }
                }
            }
            result = -1;
            return result;
        }

        private void Tab_swapTabPages(XtraTabControl tc, XtraTabPage src, XtraTabPage dst)
        {
            int newPosition = tc.TabPages.IndexOf(src);
            int newPosition2 = tc.TabPages.IndexOf(dst);
            tc.TabPages.Move(newPosition, dst);
            tc.TabPages.Move(newPosition2, src);
            tc.SelectedTabPage = src;
            tc.Refresh();
        }

        private void Tab_DragOver(object sender, DragEventArgs e)
        {
            XtraTabControl xtraTabControl = (XtraTabControl)sender;
            bool flag = e.Data.GetData(typeof(XtraTabPage)) == null;
            if (!flag)
            {
                XtraTabPage xtraTabPage = (XtraTabPage)e.Data.GetData(typeof(XtraTabPage));
                int num = xtraTabControl.TabPages.IndexOf(xtraTabPage);
                int num2 = this.Tab_getHoverTabIndex(xtraTabControl);
                bool flag2 = num2 < 0;
                if (flag2)
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    XtraTabPage xtraTabPage2 = xtraTabControl.TabPages[num2];
                    e.Effect = DragDropEffects.Move;
                    bool flag3 = xtraTabPage == xtraTabPage2;
                    if (!flag3)
                    {
                        bool flag4 = num == 0 || num2 == 0;
                        if (!flag4)
                        {
                            System.Drawing.Rectangle clientRectangle = xtraTabControl.TabPages[num].ClientRectangle;
                            System.Drawing.Rectangle clientRectangle2 = xtraTabControl.TabPages[num2].ClientRectangle;
                            bool flag5 = clientRectangle.Width < clientRectangle2.Width;
                            if (flag5)
                            {
                                System.Drawing.Point point = xtraTabControl.PointToScreen(xtraTabControl.Location);
                                bool flag6 = num < num2;
                                if (flag6)
                                {
                                    bool flag7 = e.X - point.X > clientRectangle2.X + clientRectangle2.Width - clientRectangle.Width;
                                    if (flag7)
                                    {
                                        this.Tab_swapTabPages(xtraTabControl, xtraTabPage, xtraTabPage2);
                                    }
                                }
                                else
                                {
                                    bool flag8 = num > num2;
                                    if (flag8)
                                    {
                                        bool flag9 = e.X - point.X < clientRectangle2.X + clientRectangle.Width;
                                        if (flag9)
                                        {
                                            this.Tab_swapTabPages(xtraTabControl, xtraTabPage, xtraTabPage2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.Tab_swapTabPages(xtraTabControl, xtraTabPage, xtraTabPage2);
                            }
                            xtraTabControl.SelectedTabPageIndex = xtraTabControl.TabPages.IndexOf(xtraTabPage);
                        }
                    }
                }
            }
        }

        private void PopMenu_TabPage_BeforePopup(object sender, CancelEventArgs e)
        {
            int count = xtraTabControl1.TabPages.Count;
            int index = xtraTabControl1.SelectedTabPageIndex;

            this.CloseAllTab.Enabled = true;
            this.CloseLeftTab.Enabled = true;
            this.CloseRightTab.Enabled = true;
            this.CloseOneTab.Enabled = true;
            this.CloseNoOneTab.Enabled = true;

            if (count == 1)
            {
                this.CloseAllTab.Enabled = false;
                this.CloseLeftTab.Enabled = false;
                this.CloseRightTab.Enabled = false;
                this.CloseOneTab.Enabled = false;
                this.CloseNoOneTab.Enabled = false;
            }
            else
            {
                if (index == 0)
                {
                    this.CloseOneTab.Enabled = false;
                    this.CloseLeftTab.Enabled = false;
                }

                if (index == 1)
                {
                    this.CloseLeftTab.Enabled = false;
                }

                if (index == xtraTabControl1.TabPages.Count - 1)
                {
                    this.CloseRightTab.Enabled = false;
                }
            }
        }

        private void TabAllPage_MouseUp(object sender, MouseEventArgs e)
        {
            bool flag = e.Button == MouseButtons.Right;
            if (flag)
            {
                XtraTabControl xtraTabControl = sender as XtraTabControl;
                string name = xtraTabControl.SelectedTabPage.Name;

                bool flag2 = xtraTabControl != null;
                if (flag2)
                {
                    System.Drawing.Point location = e.Location;
                    XtraTabHitInfo xtraTabHitInfo = xtraTabControl.CalcHitInfo(location);
                    bool flag3 = xtraTabHitInfo.HitTest == XtraTabHitTest.PageHeader;
                    if (flag3)
                    {
                        this.popupMenu4.ShowPopup(Control.MousePosition);


                    }
                }
            }
            else
            {
                bool flag4 = e.Button == MouseButtons.Left;
                if (flag4)
                {
                    this.Tab_MouseUp(sender, e);
                }
            }
        }

        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TreeListNode node = e.Node;
            int a = node.Id;
            FileDto file = fileDtos.FirstOrDefault(x => x.FileID == a);
            string path = file.FilePath;
            string filetype = file.FileType;
            string oldFileName = file.FileName;
            string newFileName = e.Value.ToString();
            if (newFileName == "")
            {
                e.Node.SetValue(e.Column, oldFileName);
                treeList1.OptionsBehavior.Editable = false;
                return;
            }

            if (filetype == "Folder")
            {
                string sourceDirectory = Path.GetDirectoryName(path);
                string destinationDirectory = Path.Combine(sourceDirectory, newFileName);
                Directory.Move(path, destinationDirectory);
            }
            else
            {

                string destinationFilePath = Path.Combine(Path.GetDirectoryName(path), newFileName);
                File.Move(path, destinationFilePath);
            }
            treeList1.OptionsBehavior.Editable = false;
        }

        private void FrmFolder_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void FrmFolder_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged_1(object sender, TabPageChangedEventArgs e)
        {
            if (!IsSel) return;
            if (!changeFlag) return;
            UpdIsLocation();
            string name = e.Page.Text;
            if (name == "首页") return;


            string path = Pairs.FirstOrDefault(x => x.Value == e.Page).Key;
            TabPageDto tabPage = tabPaged.FirstOrDefault(x => x.TabPath == path);
            FileDto fileDto = fileDtos.FirstOrDefault(x => x.FilePath == path);
            tabPage.IsLocation = true;
            bool flag = CheckTabPageControl(e.Page);
            if (!flag)
            {
                SplashScreenManager.ShowForm(typeof(SplashScreen1));
                AddControl addControl = new AddControl();
                e.Page.Controls.Add(addControl.AddControls(path, fileDto.FileParentType, fileDto.FileType));
                SplashScreenManager.CloseForm(true);
            }
        }

        private void treeList1_BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            int id = e.Node.Id;
            TreeViewDto treeView = fileList.FirstOrDefault(x => x.Id == id);
            treeView.IsExpand = true;
            e.Node.ImageIndex = imageList.Images.Count - 1;
            e.Node.SelectImageIndex = imageList.Images.Count - 1;
        }

        private void treeList1_BeforeCollapse(object sender, BeforeCollapseEventArgs e)
        {
            int id = e.Node.Id;
            TreeViewDto treeView = fileList.FirstOrDefault(x => x.Id == id);

            treeView.IsExpand = false;
            e.Node.ImageIndex = 0;
            e.Node.SelectImageIndex = 0;
        }

        private void groupControl2_CustomButtonChecked(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {

        }

        private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            groupControl2.Visible = false;
            splitter1.Visible= false;
            SetProperty("false");
        }

        private void FileProperty_ItemClick(object sender, ItemClickEventArgs e)
        {
            groupControl2.Visible = true;
            splitter1.Visible = true;
            SetProperty("true");
        }

        public void SetProperty(string flag)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowProperty"].Value = flag;
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新加载配置
            System.Configuration.ConfigurationManager.RefreshSection("AppSettings");
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}