using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WeddingSite.Models
{
    public partial class FAQ
    {
        public int FAQId { get; set; }
        public string Question { get; set; }
        [AllowHtml]
        public string Answer { get; set; }
    }
}
