using System.ComponentModel.DataAnnotations;

namespace ChatApi.Domain.Requests
{
    public class TokenRequest
    {
        public TokenRequest() { }

        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
