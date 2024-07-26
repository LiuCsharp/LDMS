using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ScrollHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDMS
{
    public partial class FrmTxt : DevExpress.XtraEditors.XtraForm
    {
        public string _path;
        public FrmTxt()
        {
            InitializeComponent();
        }
        Graphics g;
        public FrmTxt(string path)
        {
            InitializeComponent();
            g=panelControl1.CreateGraphics();
            this._path = path;
        }
        DevExpress.XtraEditors.ScrollHelpers.ScrollBarEditorsAPIHelper helper1;
        private void FrmTxt_Load(object sender, EventArgs e)
        {
            FieldInfo fi = typeof(MemoEdit).GetField("scrollHelper", BindingFlags.NonPublic | BindingFlags.Instance);
            helper1 = fi.GetValue(memoEdit1) as DevExpress.XtraEditors.ScrollHelpers.ScrollBarEditorsAPIHelper;
            helper1.VScroll.ValueChanged += VScroll_ValueChanged;
            
            this.panelControl2.BackColor = Color.FromArgb(228, 228, 228);
            SetMemoValue();           
            //ShowLineNo();
        }

        private void VScroll_ValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.VScrollBar scrollBar = sender as DevExpress.XtraEditors.VScrollBar;
            ScrollEventArgs args = new ScrollEventArgs(ScrollEventType.ThumbPosition, scrollBar.Value);
            MemoEdit memo = ((MemoEdit)scrollBar.Parent);
            ScrollBarEditorsAPIHelper helper = helper1;
            
            if (memo == memoEdit1) 
            {
                g.Dispose();
                g = panelControl1.CreateGraphics();
                ShowLineNo(g);
            }
                
            /*helper.VScroll.Value = scrollBar.Value;
            MethodInfo mi = typeof(ScrollBarEditorsAPIHelper).GetMethod("UpdateOriginalScroll", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(helper, new object[] { args, false });*/
        }

        public void SetMemoValue()
        {
            FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string fileInfo = sr.ReadToEnd();
            this.memoEdit1.EditValue = fileInfo.ToString();

        }

        private void memoEdit1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //memoEdit1.Select();
            //memoEdit1.Focus();
            g = panelControl1.CreateGraphics();
            //ShowLineNo(g);
        }

        private void ShowLineNo(Graphics graphics)
        {
            Point p = this.memoEdit1.Location;
            int crntFirstIndex = this.memoEdit1.GetCharIndexFromPosition(p);
            int crntFirstLine = this.memoEdit1.GetLineFromCharIndex(crntFirstIndex);
            Point crntFirstPos = this.memoEdit1.GetPositionFromCharIndex(crntFirstIndex);
            p.Y += this.memoEdit1.Height;
            int crntLastIndex = this.memoEdit1.GetCharIndexFromPosition(p);
            int crntLastLine = this.memoEdit1.GetLineFromCharIndex(crntLastIndex);
            Point crntLastPos = this.memoEdit1.GetPositionFromCharIndex(crntLastIndex);
            //graphics = this.panelControl1.CreateGraphics();
            Font font = new Font(this.memoEdit1.Font, this.memoEdit1.Font.Style);
            SolidBrush brush = new SolidBrush(Color.Green);
            Rectangle rect = this.panelControl1.ClientRectangle;
            brush.Color = this.panelControl1.BackColor;
            graphics.FillRectangle(brush, 0, 0, this.panelControl1.ClientRectangle.Width, this.panelControl1.ClientRectangle.Height);
            brush.Color = Color.FromArgb(128, 128, 128);//重置画笔颜色
            int lineSpace = 0;
            if (crntFirstLine != crntLastLine)
            {
                lineSpace = (crntLastPos.Y - crntFirstPos.Y) / (crntLastLine - crntFirstLine);
            }
            else
            {
                lineSpace = Convert.ToInt32(this.memoEdit1.Font.Size);
            }

            int brushX = 15;
            int width = this.panelControl1.Width;
            //int brushY = crntLastPos.Y + Convert.ToInt32(font.Size * 0.21f);
            int brushY = 0;
            for (int i = crntFirstLine; i < crntLastLine; i++)
            {
                graphics.DrawString((i + 1).ToString(), font, brush, brushX, brushY);
                brushY += lineSpace;                
            }
            //g.Dispose();
            font.Dispose();
            brush.Dispose();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            ShowLineNo(g);
        }

        private void FrmTxt_Paint(object sender, PaintEventArgs e)
        {
            //ShowLineNo();
        }
    }
}