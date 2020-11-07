using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IEntityType
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
