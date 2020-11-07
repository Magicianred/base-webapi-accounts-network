using Magicianred.Accounts.Domain.Interfaces.Models.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models.Filters
{
    public interface IUserEntityFilter : IFilter
    {
        int? UserId { get; set; }
        int? EntityId { get; set; }
        bool? IsOwner { get; set; }
    }
}
