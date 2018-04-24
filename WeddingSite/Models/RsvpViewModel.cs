using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeddingSite.Models
{
    public class RsvpViewModel
    {
        public string Name { get; set; }
        public string GuestCode { get; set; }
        public int AllowedGuests { get; set; }
        public int NumberOfGuests { get; set; }
        public bool? Attending { get; set; }
    }
}