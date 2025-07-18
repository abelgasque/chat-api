using System;
using System.ComponentModel.DataAnnotations;

namespace ChatApi.Domain.Entities.Models
{
    public class BaseModel
    {
        public long Id { get; set; }

        [Required]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}