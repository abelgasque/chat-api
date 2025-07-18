using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Models
{
    [Table("Users")]
    public class UserModel
    {
        public UserModel() { }

        public long Id { get; set; }

        [Required]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

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

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
