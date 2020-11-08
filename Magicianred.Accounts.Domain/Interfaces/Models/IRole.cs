using System.Collections.Generic;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IRole
    {
        int Id { get; set; }
        string Name { get; set; }

        string Description { get; set; }

        List<IAccountRole> AccountRoles { get; set; }
    }
}
