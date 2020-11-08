using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            AccountRoles = new HashSet<AccountRoles>();
            UserAccounts = new HashSet<UserAccounts>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<AccountRoles> AccountRoles { get; set; }
        public virtual ICollection<UserAccounts> UserAccounts { get; set; }
    }
}
