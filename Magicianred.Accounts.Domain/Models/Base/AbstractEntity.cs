using Magicianred.Accounts.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Models.Base
{
    public abstract class AbstractEntity : IEntity
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Properties { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
