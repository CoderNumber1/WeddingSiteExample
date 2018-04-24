using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace WeddingSite.Models
{
    public partial class UserLogin : IdentityUserLogin<int>
    {
        public virtual User User { get; set; }
    }
}
