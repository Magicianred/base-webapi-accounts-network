using Magicianred.Accounts.Domain.Interfaces.Models.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models.Filters
{
    public interface IRoleFilter : IFilter
    {
        int? Id { get; set; }
        string Name { get; set; }

        string Description { get; set; }
    }
}
