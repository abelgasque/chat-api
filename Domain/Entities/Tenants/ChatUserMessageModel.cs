using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Tenants
{
    [Table("ChatUserMessages")]
    public class ChatUserMessageModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public Guid ReceiverId { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime? SentAt { get; set; }

        public DateTime? ReceiverAt { get; set; }

        public DateTime? ReadAt { get; set; }
    }
}

