using Magicianred.Accounts.Domain.Models.Communication;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Interfaces.Services.Communication
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string email, string password);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail);
        void RevokeRefreshToken(string refreshToken);
    }
}