using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class Blob
    {
        public Blob()
        {
            this.BannerBlobs = new List<BannerBlob>();
        }

        public int BlobId { get; set; }
        public Nullable<int> BlobContainerId { get; set; }
        public string AltTag { get; set; }
        public string DisplayName { get; set; }
        public string URL { get; set; }
        public virtual ICollection<BannerBlob> BannerBlobs { get; set; }
        public virtual BlobContainer BlobContainer { get; set; }
    }
}
