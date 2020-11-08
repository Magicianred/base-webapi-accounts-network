using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class Users
    {
        public Users()
        {
            UserAccounts = new HashSet<UserAccounts>();
            UserEntities = new HashSet<UserEntities>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Properties { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastAccess { get; set; }

        public virtual ICollection<UserAccounts> UserAccounts { get; set; }
        public virtual ICollection<UserEntities> UserEntities { get; set; }
    }
}
