using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4_05_HW_SimplePictureLikes.Data;

namespace _4_05_HW_SimplePictureLikes.Web.Models
{
    public class ImageViewModel
    {
        public bool Likable { get; set; }
        public Image Image { get; set; }
        public List<Image> Images { get; set; }
    }
}
