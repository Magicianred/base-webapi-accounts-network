using System.ComponentModel.DataAnnotations.Schema;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IAccountRole
    {
        int AccountId { get; set; }

        IAccount Account { get; set; }

        int RoleId { get; set; }

        IRole Role { get; set; }
    }
}
