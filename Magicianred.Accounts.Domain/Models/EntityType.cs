using Magicianred.Accounts.Domain.Interfaces.Models;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.Accounts.Domain.Models
{
    public class EntityType : IEntityType
    {
        public EntityType()
        {
            Entities = new HashSet<Entity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
        List<IEntity> IEntityType.Entities
        {
            get
            {
                return Entities != null ? this.Entities.ToList<IEntity>() : null;
            }
            set
            {
                var list = new List<Entity>();
                foreach (var item in value)
                {
                    list.Add((Entity)item);
                }
                this.Entities = list;
            }
        }
    }
}
