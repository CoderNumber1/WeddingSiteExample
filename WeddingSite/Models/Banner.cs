using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class Banner
    {
        public Banner()
        {
            this.BannerBlobs = new List<BannerBlob>();
        }

        public int BannerId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BannerBlob> BannerBlobs { get; set; }
    }
}
