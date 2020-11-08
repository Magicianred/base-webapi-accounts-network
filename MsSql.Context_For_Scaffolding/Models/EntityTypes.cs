using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class EntityTypes
    {
        public EntityTypes()
        {
            Entities = new HashSet<Entities>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Entities> Entities { get; set; }
    }
}
