using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IUserAccount
    {
        int UserId { get; set; }
        int AccountId { get; set; }
    }
}
