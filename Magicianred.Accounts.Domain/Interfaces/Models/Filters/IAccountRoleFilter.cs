using Magicianred.Accounts.Domain.Interfaces.Models.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models.Filters
{
    public interface IAccountRoleFilter : IFilter
    {
        int? AccountId { get; set; }

        int? RoleId { get; set; }
    }
}
