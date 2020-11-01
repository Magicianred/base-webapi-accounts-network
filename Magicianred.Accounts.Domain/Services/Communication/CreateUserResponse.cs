using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Services.Communication;
using Magicianred.Accounts.Domain.Models.Communication;

namespace Magicianred.Accounts.Domain.Services.Communication
{
    public class CreateUserResponse : BaseResponse, ICreateUserResponse
    {
        public IAccount Account { get; private set; }

        public CreateUserResponse(bool success, string message, IAccount account) : base(success, message)
        {
            Account = account;
        }
    }
}