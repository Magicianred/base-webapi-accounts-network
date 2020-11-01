using System.ComponentModel.DataAnnotations;

namespace Magicianred.Accounts.WebApi.Models.Resources
{
    public class RevokeTokenResource
    {
        [Required]
        public string Token { get; set; }
    }
}