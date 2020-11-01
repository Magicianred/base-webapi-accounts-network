using Magicianred.Accounts.Domain.Models.Security;

namespace Magicianred.Accounts.Domain.Models.Communication
{
    public class TokenResponse : BaseResponse
    {
        public AccessToken Token { get; set; }

        public TokenResponse(bool success, string message, AccessToken token) : base(success, message)
        {
            Token = token;
        }
    }
}