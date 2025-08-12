using System;
using System.Collections.Generic;
using ChatApi.Domain.Entities.Tenants;

namespace ChatApi.Domain.Responses
{
    public class ChatResponse : ChatModel
    {
        public ChatResponse(ChatModel model)
        {
            Id = model.Id;
            Name = model.Name;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            DeletedAt = model.DeletedAt;
            SenderId = model.SenderId;
            ReceiverId = model.ReceiverId;
        }

        public List<ChatMessageModel> Messages { get; set; }
    }
}
