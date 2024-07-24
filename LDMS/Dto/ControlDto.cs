using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyTxtInfo.Dto
{
    public class ControlDto
    {
        public string Path { get; set; }

        public List<TreeViewDto> TreeViewDto { get; set; }

        public List<ShortcutsControl> ShortcutsControl  { get; set; }

        public List<TabPageDto> TabPageDto { get; set; }

        public List<FileDto> FileDto { get; set; }

        //public ImageList ImageList { get; set; }
    }
}
