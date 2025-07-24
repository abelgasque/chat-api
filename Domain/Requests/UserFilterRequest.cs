using System.ComponentModel.DataAnnotations;
using ChatApi.Domain.Responses;

namespace ChatApi.Domain.Requests
{
    public abstract class UserFilterRequest : PaginationRequest
    {
        public bool? Active { get; set; }
    }
}
