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

        public virtual Entity Entity { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute]
        IEntity IUserEntity.Entity { get => this.Entity; set { this.Entity = (Entity)value; } }

        public virtual User User { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute]
        IUser IUserEntity.User { get => this.User; set { this.User = (User)value; } }
    }
}
