namespace ChatApi.Domain.Requests
{
    public class UserFilterRequest : PaginationRequest
    {
        public bool? Active { get; set; }
    }
}
