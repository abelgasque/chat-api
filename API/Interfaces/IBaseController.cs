using System;
using System.Threading.Tasks;
using ChatApi.Domain.Requests;
using ChatApi.Domain.Responses;

namespace ChatApi.API.Interfaces
{
    public interface IBaseController<TModel>
    {
        Task CreateAsync(TModel entity);
        Task<TModel> ReadById(Guid id);
        Task<PaginationResponse> Read(PaginationRequest filter);
        Task UpdateAsync(TModel entity);
        Task DeleteAsync(Guid id);
    }
}