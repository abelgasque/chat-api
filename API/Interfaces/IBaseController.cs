using System;
using System.Threading.Tasks;

namespace ChatApi.API.Interfaces
{
    public interface IBaseController<TModel>
    {
        Task CreateAsync(TModel entity);
        Task<TModel> ReadById(Guid id);
        Task UpdateAsync(TModel entity);
        Task DeleteAsync(Guid id);
    }
}