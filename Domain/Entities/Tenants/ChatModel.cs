using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ChatApi.Domain.Entities.Models;

namespace ChatApi.Domain.Entities.Tenants
{
    [Table("Chats")]
    public class ChatModel : BaseModel
    {
        public string MeId { get; set; }
        public string MeName { get; set; }
        public string MeJid { get; set; }

        public Guid ChannelId { get; set; }
        public string ContactId { get; set; }
        public string ContactName { get; set; }

        public List<ChatMessageModel> Messages { get; set; }
    }
}