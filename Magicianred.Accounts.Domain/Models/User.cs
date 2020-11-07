using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Models.Base;
using System;

namespace Magicianred.Accounts.Domain.Models
{
    public class User : AbstractEntity, IUser, IEntity
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
