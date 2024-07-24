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
        xls,
        doc,
        pdf,
        txt,
        jpg
    }
}