using Magicianred.Accounts.Domain.Interfaces.Models;
using System;

namespace Magicianred.Accounts.Domain.Models
{
    public class UserEntity : IUserEntity
    {
        public int UserId { get; set; }
        public int EntityId { get; set; }
        public bool IsOwner { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
