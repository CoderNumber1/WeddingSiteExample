using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingSite.Models.GuestApi
{
    public class GuestCodeRequest
    {
        public int? GuestId { get; set; }
        public int? UseCount { get; set; }
    }
}