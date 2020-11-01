using Magicianred.Accounts.Domain.Interfaces.Models;

namespace Magicianred.Accounts.Domain.Interfaces.Services.Communication
{
    public interface ICreateUserResponse
    {
        IAccount Account { get; }
    }
}

