using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class UserEntities
    {
        public int UserId { get; set; }
        public int EntityId { get; set; }
        public bool IsOwner { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Entities Entity { get; set; }
        public virtual Users User { get; set; }
    }
}
