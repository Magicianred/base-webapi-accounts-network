using System;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IUser : IEntity
    {
        string Username { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        DateTime LastAccess { get; set; }
    }
}
