using System;
using System.ComponentModel.DataAnnotations.Schema;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Tenants
{
    [Table("ChatMessages")]
    public class ChatMessageModel
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }
    }
}