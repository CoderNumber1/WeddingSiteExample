using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingSite.Models.PhotoApi
{
    public class PhotoPackage
    {
        public string LibraryName { get; set; }
        public string ExtraLarge { get; set; }
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Small { get; set; }
        public string ThumbNail { get; set; }
    }
}