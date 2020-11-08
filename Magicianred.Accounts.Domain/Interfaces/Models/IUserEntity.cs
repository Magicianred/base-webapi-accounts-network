using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Interfaces.Models
{
    public interface IUserEntity
    {
        int UserId { get; set; }
        int EntityId { get; set; }
        bool IsOwner { get; set; }
        DateTime CreateDate { get; set; }
        IEntity Entity { get; set; }
        IUser User { get; set; }
    }
}
