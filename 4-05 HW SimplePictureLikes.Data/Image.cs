using System;

namespace _4_05_HW_SimplePictureLikes.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int Likes { get; set; }
        public DateTime TimePosted { get; set; }
    }
}
