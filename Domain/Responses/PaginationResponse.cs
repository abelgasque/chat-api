using ChatApi.Domain.Requests;

namespace ChatApi.Domain.Responses
{
    public class PaginationResponse : PaginationRequest
    {
        public PaginationResponse() { }

        public int Total { get; set; }
        public object Data { get; set; }
    }
}
