using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Models.Filters;
using System.Collections.Generic;

namespace Magicianred.Accounts.Domain.Models.Filters
{
    public class AccountFilter : IAccountFilter
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public List<IAccountRole> AccountRoles { get; set; }
    }
}
