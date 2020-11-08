using Magicianred.Accounts.Domain.Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magicianred.Accounts.Domain.Models
{
    public class AccountRole : IAccountRole
    {
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        IAccount IAccountRole.Account { get => (IAccount)this.Account; set => this.Account = (Account)value; }
        public int RoleId { get; set; }


        public virtual Role Role { get; set; }

        IRole IAccountRole.Role { get => (IRole)this.Role; set => this.Role = (Role)value; }
    }
}
