using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class Entities
    {
        public Entities()
        {
            UserEntities = new HashSet<UserEntities>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Properties { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual EntityTypes Type { get; set; }
        public virtual ICollection<UserEntities> UserEntities { get; set; }
    }
}
