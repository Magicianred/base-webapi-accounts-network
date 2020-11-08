using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class UserAccounts
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Users User { get; set; }
    }
}
