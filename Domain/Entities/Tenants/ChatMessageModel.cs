using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi.Domain.Entities.Tenants
{
    [Table("ChatMessages")]
    public class ChatMessageModel
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public Guid ChatId { get; set; }

        public DateTime Timestamp { get; set; }
        public string Body { get; set; }
        public bool FromMe { get; set; }
        public string Source { get; set; }
        public bool HasMedia { get; set; }
        public int Ack { get; set; }
        public string AckName { get; set; }

        public string Engine { get; set; }

        public string SenderId { get; set; }
        public string SenderName { get; set; }

        public ChatModel Chat { get; set; }
    }
}