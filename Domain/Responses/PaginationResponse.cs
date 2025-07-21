using System.Collections.Generic;
using System.Linq;
using ChatApi.Domain.Requests;

namespace ChatApi.Domain.Responses
{
    public class PaginationResponse : PaginationRequest
    {
        public int Total { get; set; }
        public IEnumerable<object> Data { get; set; } = Enumerable.Empty<object>();
    }
}