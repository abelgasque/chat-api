using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Models
{
    [Table("Users")]
    public class UserModel : BaseModel
    {
        public UserModel() { }

        [MaxLength(500)]
        public string AvatarUrl { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime? ActiveAt { get; set; }

        public DateTime? BlockedAt { get; set; }

        [Required]
        public int NuLogged { get; set; } = 0;

        public DateTime? LoggedAt { get; set; }

        [Required]
        public int NuRefreshed { get; set; } = 0;

        public DateTime? RefreshedAt { get; set; }
    }
}
