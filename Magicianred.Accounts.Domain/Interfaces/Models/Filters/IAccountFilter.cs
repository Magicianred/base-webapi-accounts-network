using Magicianred.Accounts.Domain.Interfaces.Models.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models.Filters
{
    public interface IAccountFilter : IFilter
    {
        int? Id { get; set; }

        string Email { get; set; }

        //List<IAccountRole> AccountRoles { get; set; }
    }
}
