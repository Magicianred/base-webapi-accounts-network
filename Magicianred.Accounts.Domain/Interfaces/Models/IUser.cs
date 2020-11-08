using System;
using System.Collections.Generic;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IUser : IEntity
    {
        string Username { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        DateTime? LastAccess { get; set; }
        List<IUserAccount> UserAccounts { get; set; }
        List<IUserEntity> UserEntities { get; set; }
    }
}
