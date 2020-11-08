using System.Collections.Generic;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IEntityType
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        List<IEntity> Entities { get; set; }
    }
}
