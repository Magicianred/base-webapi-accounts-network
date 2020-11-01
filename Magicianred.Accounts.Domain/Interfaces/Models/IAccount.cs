using System;
using System.Collections.Generic;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IAccount
    {
        int Id { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        DateTime CreateDate { get; set; }

        List<IAccountRole> AccountRoles { get; set; }
    }
}
