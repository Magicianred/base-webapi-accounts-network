using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magicianred.Accounts.Domain.Models
{
    public class User : AbstractEntity, IUser, IEntity
    {
        public User()
        {
            UserAccounts = new HashSet<UserAccount>();
            UserEntities = new HashSet<UserEntity>();
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? LastAccess { get; set; }

        public virtual ICollection<UserAccount> UserAccounts { get; set; }

        List<IUserAccount> IUser.UserAccounts
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

        public virtual ICollection<UserEntity> UserEntities { get; set; }

        List<IUserEntity> IUser.UserEntities
        {
            get
            {
                return UserEntities != null ? this.UserEntities.ToList<IUserEntity>() : null;
            }
            set
            {
                var list = new List<UserEntity>();
                foreach (var item in value)
                {
                    list.Add((UserEntity)item);
                }
                this.UserEntities = list;
            }
        }
    }
}
