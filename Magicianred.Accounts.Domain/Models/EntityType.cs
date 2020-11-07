using Magicianred.Accounts.Domain.Interfaces.Models;

namespace Magicianred.Accounts.Domain.Models
{
    public class EntityType : IEntityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
