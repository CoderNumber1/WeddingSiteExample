using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class UserClaim : IdentityUserClaim<int>
    {
        public virtual User User { get; set; }
    }
}
