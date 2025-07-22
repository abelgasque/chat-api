using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApi.Domain.Requests
{
    public class RefreshTokenRequest
    {
        public RefreshTokenRequest() { }

        [Required]
        public string RefreshToken { get; set; }
    }
}
