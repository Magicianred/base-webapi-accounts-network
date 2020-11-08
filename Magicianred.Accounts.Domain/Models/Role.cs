using Magicianred.Accounts.Domain.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.Accounts.Domain.Models
{
    public class Role : IRole
    {
        public Role()
        {
            AccountRoles = new HashSet<AccountRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        List<IAccountRole> IRole.AccountRoles
        {
            get
            {
                return AccountRoles != null ? this.AccountRoles.ToList<IAccountRole>() : null;
            }
            set
            {
                var list = new List<AccountRole>();
                foreach (var item in value)
                {
                    list.Add((AccountRole)item);
                }
                this.AccountRoles = list;
            }
        }
    }
}
