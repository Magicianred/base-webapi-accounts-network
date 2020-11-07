using Magicianred.Accounts.Domain.Interfaces.Models.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models.Filters
{
    public interface IUserAccountFilter : IFilter
    {
        int? UserId { get; set; }
        int? AccountId { get; set; }
    }
}
