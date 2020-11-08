using Magicianred.Accounts.Domain.Interfaces.Models.Filters.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models.Filters
{
    public interface IUserFilter : IFilter
    {
        int? Id { get; set; }
        string Username { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
    }
}
