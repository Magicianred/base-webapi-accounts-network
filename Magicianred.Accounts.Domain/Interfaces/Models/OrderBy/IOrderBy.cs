using Magicianred.Accounts.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models.OrderBy
{
    public interface IOrderBy
    {
        string Property { get; set; }
        OrderByEnum Sort { get; set; }
    }
}
