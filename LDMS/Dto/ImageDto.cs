﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyTxtInfo.Dto
{
    public class ImageDto
    {
        public int ImageID { get; set; }

        public string ImageType { get; set; }

        public string ImageBase64String { get; set; }
    }
}
