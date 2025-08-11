using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Tenants
{
    [Table("Chats")]
    public class ChatModel : BaseModel
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public List<ChatMessageModel> Messages { get; set; }
    }
}