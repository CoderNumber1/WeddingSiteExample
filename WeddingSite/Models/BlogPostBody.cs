using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class BlogPostBody
    {
        public int BlogPostBodyId { get; set; }
        public int BlogPostId { get; set; }
        public int OrderBy { get; set; }
        public string Content { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}
