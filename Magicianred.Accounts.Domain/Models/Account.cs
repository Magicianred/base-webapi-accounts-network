using Magicianred.Accounts.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.Accounts.Domain.Models
{
    public class Account : IAccount
    {
        public Account()
        {
            AccountRoles = new HashSet<AccountRole>();
            UserAccounts = new HashSet<UserAccount>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute]
        public User User { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute]
        IUser IAccount.User { get => this.User; set { this.User = (User)value; } }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }

        List<IAccountRole> IAccount.AccountRoles
        {
            get
            {
                return AccountRoles != null ? this.AccountRoles.ToList<IAccountRole>() : null;
            }
            set
            {
                var list = new List<AccountRole>();
                foreach (var item in value)
                {
                    list.Add((AccountRole)item);
                }
                this.AccountRoles = list;
            }
        }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }

        List<IUserAccount> IAccount.UserAccounts
        {
            get
            {
                return UserAccounts != null ? this.UserAccounts.ToList<IUserAccount>() : null;
            }
            set
            {
                var list = new List<UserAccount>();
                foreach (var item in value)
                {
                    list.Add((UserAccount)item);
                }
                this.UserAccounts = list;
            }
        }

    }
}
