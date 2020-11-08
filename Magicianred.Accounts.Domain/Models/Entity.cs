using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magicianred.Accounts.Domain.Models
{
    public class Entity : AbstractEntity, IEntity
    {
        public Entity()
        {
            UserEntities = new HashSet<UserEntity>();
        }
        [NotMapped]
        public virtual EntityType Type { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute]
        IEntityType IEntity.Type { get => this.Type; set { this.Type = (EntityType)value; } }

        public virtual ICollection<UserEntity> UserEntities { get; set; }
    }
}
