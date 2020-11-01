using Magicianred.Accounts.Domain.Interfaces.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magicianred.Accounts.Domain.Models
{
    public class AccountRole : IAccountRole
    {
        public int AccountId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public Account Account { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        IAccount IAccountRole.Account { get => (IAccount)this.Account; set => this.Account = (Account)value; }
        public int RoleId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IRole Role { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        IRole IAccountRole.Role { get => (IRole)this.Role; set => this.Role = (Role)value; }
    }
}
