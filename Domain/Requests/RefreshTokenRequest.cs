using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApi.Domain.Requests
{
    public class RefreshTokenRequest
    {
        public RefreshTokenRequest() { }

        [Required]
        public string AccessToken { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}
