using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyTxtInfo.Dto
{
    /// <summary>
    /// 树实体类
    /// </summary>
    public class TreeViewDto
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string TName { get; set; }

        public bool IsExpand { get; set; } = false;

        //public Image Image { get; set; }
    }
}
