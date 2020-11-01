using Magicianred.Accounts.Domain.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.Accounts.Domain.Models
{
    public class Account : IAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public List<AccountRole> AccountRoles { get; set; }
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
    }
}
