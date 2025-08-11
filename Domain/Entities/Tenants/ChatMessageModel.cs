using System;
using System.ComponentModel.DataAnnotations.Schema;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Tenants
{
    [Table("ChatMessages")]
    public class ChatMessageModel : BaseModel
    {
        public Guid ChatId { get; set; }
        public string Message { get; set; }

        public ChatModel Chat { get; set; }
    }
}