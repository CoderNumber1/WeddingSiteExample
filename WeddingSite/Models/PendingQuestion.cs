using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class PendingQuestion
    {
        public int Id { get; set; }
        public string ReplyEmail { get; set; }
        public string Question { get; set; }
        public bool WillAnswer { get; set; }
    }
}
