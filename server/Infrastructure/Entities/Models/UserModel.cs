using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Infrastructure.Entities.Models
{
    public class UserModel
    {
        public UserModel() { }

        public long Id { get; set; }

        public Guid guid { get; set; }

        [Required]      
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(250)]        
        public string Mail { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public int AuthAttempts { get; set; }

        public DateTime? ActiveAt { get; set; }

        public DateTime? BlockedAt { get; set; }
    }
}
