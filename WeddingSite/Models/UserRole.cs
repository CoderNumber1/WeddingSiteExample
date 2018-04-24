using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class UserRole : IdentityRole<int, UserUserRole>
    {
        public UserRole()
            : base()
        {
        }
    }
}
