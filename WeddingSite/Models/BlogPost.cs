using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class BlogPost
    {
        public BlogPost()
        {
            this.BlogPostBodies = new List<BlogPostBody>();
        }

        public int BlogPostId { get; set; }
        public string Title { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public virtual ICollection<BlogPostBody> BlogPostBodies { get; set; }
    }
}
