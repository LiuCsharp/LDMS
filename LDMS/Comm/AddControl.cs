using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using DevExpress.XtraBars;
using DevExpress.Data.ExpressionEditor;
using LDMS;
using DevExpress.XtraPdfViewer;

namespace CopyTxtInfo.Comm
{
    public class AddControl : FrmFolder
    {
        private  Control control = new Control();

        public AddControl()
        {
            
        }
      
        public  Control AddControls(string filepath,string fileParentType,string filetype) 
        {

            switch (fileParentType) 
            {
                case "text":
                    control = AddMemoEdit(filepath);
                    break;
                case "office":
                    control = AddOffice(filepath,filetype);
                    break;
                case "image":
                    control = AddImage(filepath);
                    break;
                default:
                    control = AddLabel();
                    break;
            }
            return control;
        }

        private  Label AddLabel() 
        {
            Label label = new Label();
            label.Text = "该文件暂不支持预览";
            label.Dock = DockStyle.Fill;
            label.Font = new Font("黑体",10);
            label.AutoSize = true;
            return label;
        }

        private  MemoEdit AddMemoEdit(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string fileInfo = sr.ReadToEnd();
            MemoEdit memoEdit = new MemoEdit();
            memoEdit.Text = fileInfo.ToString();
            memoEdit.Dock = DockStyle.Fill;
            memoEdit.KeyUp += MemoEdit_KeyUp;
            
            //memoEdit.ReadOnly = true;                       
            return memoEdit;
        }

        

        private  Control AddOffice(string filepath,string filetype)
        {
            string _filetype=filetype.Substring(0,3);
            Control _control = new Control();
            if (_filetype == "doc") 
            {
                RichEditControl richEdit = new RichEditControl();
                /*DocumentRange range = richEdit.Document.Range;
                CharacterProperties cp = richEdit.Document.BeginUpdateCharacters(range);
                richEdit.Document.EndUpdateCharacters(cp);*/
                richEdit.Dock = DockStyle.Fill;
                richEdit.LoadDocument(filepath);             
                _control = richEdit;
            }

            if (_filetype == "xls") 
            {
                SpreadsheetControl spreadsheet = new SpreadsheetControl();
                IWorkbook workbook = spreadsheet.Document;
                workbook.LoadDocument(filepath);
                spreadsheet.Dock = DockStyle.Fill;                              
                _control = spreadsheet;
            }


            if (_filetype == "pdf") 
            {
                PdfViewer pdfViewer = new PdfViewer();
                pdfViewer.Dock = DockStyle.Fill;
                pdfViewer.LoadDocument(filepath);
                _control = pdfViewer;
            }
            return _control;
        }
      
        private PictureEdit AddImage(string filepath)
        {
            /*PanelControl panelControl = new PanelControl();
            panelControl.Dock = DockStyle.Fill;*/
            PictureEdit pictureEdit = new PictureEdit();
            pictureEdit.Image = new Bitmap(filepath);
            pictureEdit.Dock = DockStyle.Fill;
            pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit.Properties.ShowMenu = false;
            //panelControl.Controls.Add(pictureEdit);
            return pictureEdit;
        }

      
    }
}
