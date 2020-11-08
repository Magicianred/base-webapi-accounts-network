using Magicianred.Accounts.Domain.Interfaces.Services.Communication;
using Magicianred.Accounts.WebApi.Extensions;
using Magicianred.Accounts.WebApi.Models.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Magicianred.Accounts.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("ValidationAuth")]
        public String ValidationAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    foreach (Claim c in claims)
                    {
                        Console.WriteLine(c.Type + " = " + c.Value);
                    }
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost("SecureEndpoint")]
        public Object SecureEndpoint()
        {
            if (User.Identity is ClaimsIdentity identity)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }
            return null;
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.CreateAccessTokenAsync(userCredentials.Email, userCredentials.Password);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var accessTokenResource = response.Token.ToResourceAccessToken();
            return Ok(accessTokenResource);
        }


        //[Route("/api/token/refresh")]
        //[HttpPost]
        //public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource refreshTokenResource)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var response = await _authenticationService.RefreshTokenAsync(refreshTokenResource.Token, refreshTokenResource.UserEmail);
        //    if (!response.Success)
        //    {
        //        return BadRequest(response.Message);
        //    }

        //    var tokenResource = response.Token.ToResourceAccessToken();
        //    return Ok(tokenResource);
        //}

        //[Route("/api/token/revoke")]
        //[HttpPost]
        //public IActionResult RevokeToken([FromBody] RevokeTokenResource revokeTokenResource)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _authenticationService.RevokeRefreshToken(revokeTokenResource.Token);
        //    return NoContent();
        //}
    }
}
