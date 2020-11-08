using System;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IEntity
    {
        int Id { get; set; }
        int TypeId { get; set; }
        IEntityType Type { get; set; }

        string Title { get; set; }
        string Description { get; set; }
        string Properties { get; set; }
        DateTime CreateDate { get; set; }

    }
}
