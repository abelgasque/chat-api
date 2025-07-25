using System;

namespace ChatApi.Domain.Requests
{
    public class UserMessageFilterRequest : PaginationRequest
    {
        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }
    }
}
