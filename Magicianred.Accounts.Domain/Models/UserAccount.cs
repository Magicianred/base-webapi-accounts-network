using Magicianred.Accounts.Domain.Interfaces.Models;

namespace Magicianred.Accounts.Domain.Models
{
    public class UserAccount : IUserAccount
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
    }
}
