using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApi.Domain.Entities.DTO
{
    public class RefreshTokenDTO
    {
        public RefreshTokenDTO() { }

        [Required]
        public string AccessToken { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}
