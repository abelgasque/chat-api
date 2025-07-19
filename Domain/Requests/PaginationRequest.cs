namespace ChatApi.Domain.Requests
{
    public class PaginationRequest
    {
        public PaginationRequest() { }

        public int Page { get; set; } = 1;
        public int Size { get; set; } = 25;
    }
}
