using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class AccountRoles
    {
        public int AccountId { get; set; }
        public int RoleId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Roles Role { get; set; }
    }
}
