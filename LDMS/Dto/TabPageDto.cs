using DevExpress.XtraRichEdit.Import.Rtf;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyTxtInfo.Dto
{
    public class TabPageDto
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public int TabID { get; set; }
        /// <summary>
        /// 页签标题
        /// </summary>
        public string TabText { get; set; }

        /// <summary>
        /// 页签名称
        /// </summary>
        public string TabName { get; set; }

        /// <summary>
        /// 页签索引
        /// </summary>
        public int TabIndex { get; set; }

        public bool IsLocation { get; set; } = false;

        public string TabPath { get; set; }

        /// <summary>
        /// 页签
        /// </summary>
        //public XtraTabPage tabPage { get; set; }
    }
}
