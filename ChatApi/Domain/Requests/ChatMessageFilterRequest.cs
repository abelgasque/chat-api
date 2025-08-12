using System;

namespace ChatApi.Domain.Requests
{
    public class ChatMessageFilterRequest : PaginationRequest
    {
        public Guid? ChatId { get; set; }
    }
}
