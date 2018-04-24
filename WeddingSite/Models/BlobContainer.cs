using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class BlobContainer
    {
        public BlobContainer()
        {
            this.Blobs = new List<Blob>();
        }

        public int BlobContainerId { get; set; }
        public string ContainerName { get; set; }
        public string DisplayName { get; set; }
        public virtual ICollection<Blob> Blobs { get; set; }
    }
}
