using Magicianred.Accounts.Domain.Interfaces.Models.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magicianred.Accounts.Domain.Models.Filters
{
    public class UserFilter : IUserFilter
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
