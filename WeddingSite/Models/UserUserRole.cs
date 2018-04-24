using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class UserUserRole : IdentityUserRole<int>
    {
        //public int ID { get; set; }
        //public int UserID { get; set; }
        //public int RoleID { get; set; }
        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
