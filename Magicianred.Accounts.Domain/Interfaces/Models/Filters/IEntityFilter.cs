using Magicianred.Accounts.Domain.Interfaces.Models.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models.Filters
{
    public interface IEntityFilter : IFilter
    {
        int? Id { get; set; }
        int? TypeId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Properties { get; set; }
    }
}
