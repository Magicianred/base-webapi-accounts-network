using Magicianred.Accounts.Domain.Interfaces.Models;

namespace Magicianred.Accounts.Domain.Models
{
    public class UserAccount : IUserAccount
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }
        IUser IUserAccount.User { get => (IUser)this.User; set => this.User = (User)value; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
        IAccount IUserAccount.Account { get => (IAccount)this.Account; set => this.Account = (Account)value; }

    }
}
