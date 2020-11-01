using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Models.Security;

namespace Magicianred.Accounts.Domain.Interfaces.Handlers
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(IAccount account);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}