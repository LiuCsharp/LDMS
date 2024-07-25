using DevExpress.XtraRichEdit.Import.Rtf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyTxtInfo.Dto
{
    /// <summary>
    /// 文件实体类
    /// </summary>
    public class FileDto
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        public int FileID { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>

        public string FileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>

        public string FileType { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>

        public string FileInfo { get; set; }

        /// <summary>
        /// 文件父类型
        /// </summary>
        public string FileParentType { get; set; }

        //public Image FileImage { get; set; }

        public long FileSize { get; set; }
    }
    public  enum FileParentType
    {
        office = 0,
        image= 1,
        text=2,
        video= 3,
    }

    public enum FileType 
    {
        Folder = 0,
        Txt= 1,
        Doc= 2,
        Docx= 3,
        xls= 4,
        xlsx=5,
        pdf= 6,
        html= 7,
        exe= 8,

    }

    public enum PropertyCode 
    {
        文件名称= 0,
        文件路径= 1,
        文件类型 = 2,
        文件大小 = 3,
    }
}