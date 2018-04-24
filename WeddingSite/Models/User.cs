using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class User : IdentityUser<int, UserLogin, UserUserRole, UserClaim>
    {
        public User()
            : base()
        {
        }
    }
}
