using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class Roles
    {
        public Roles()
        {
            AccountRoles = new HashSet<AccountRoles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AccountRoles> AccountRoles { get; set; }
    }
}
