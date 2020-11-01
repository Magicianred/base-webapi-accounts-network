using Magicianred.Accounts.Domain.Models.Security;
using Magicianred.Accounts.WebApi.Models.Resources;

namespace Magicianred.Accounts.WebApi.Extensions
{
    public static class AccessTokenExtension
    {
        public static AccessTokenResource ToResourceAccessToken(this AccessToken data)
        {
            return new AccessTokenResource()
            {
                AccessToken = data.Token,
                Expiration = data.Expiration,
                RefreshToken = data.RefreshToken.ToString()
            };
        }
    }
}