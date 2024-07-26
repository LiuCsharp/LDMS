using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDMS.Dto
{
    public class AddFilePath
    {
        public string FilePath { get; set; }

        public int OpenNumber { get; set; }

        public bool LastOpen { get; set; }
    }
}
