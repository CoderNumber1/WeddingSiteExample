using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class BannerBlob
    {
        public int BannerBlobId { get; set; }
        public int BannerId { get; set; }
        public int BlobId { get; set; }
        public virtual Banner Banner { get; set; }
        public virtual Blob Blob { get; set; }
    }
}
