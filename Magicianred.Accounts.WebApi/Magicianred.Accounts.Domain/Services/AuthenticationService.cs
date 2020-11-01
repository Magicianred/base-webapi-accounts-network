using Magicianred.Accounts.Domain.Interfaces;
using Magicianred.Accounts.Domain.Interfaces.Handlers;
using Magicianred.Accounts.Domain.Interfaces.Services.Communication;
using Magicianred.Accounts.Domain.Models.Communication;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IAccountService accountService, IPasswordHandler passwordHandler, ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _passwordHandler = passwordHandler;
            _accountService = accountService;
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string email, string password)
        {
            var account = await _accountService.FindByEmailAsync(email);

            if (account == null || !_passwordHandler.PasswordMatches(password, account.Password))
            {
                return new TokenResponse(false, "Invalid credentials.", null);
            }

            var token = _tokenHandler.CreateAccessToken(account);

            return new TokenResponse(true, null, token);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string userEmail)
        {
            var token = _tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null)
            {
                return new TokenResponse(false, "Invalid refresh token.", null);
            }

            if (token.IsExpired())
            {
                return new TokenResponse(false, "Expired refresh token.", null);
            }

            var account = await _accountService.FindByEmailAsync(userEmail);
            if (account == null)
            {
                return new TokenResponse(false, "Invalid refresh token.", null);
            }

            var accessToken = _tokenHandler.CreateAccessToken(account);
            return new TokenResponse(true, null, accessToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}