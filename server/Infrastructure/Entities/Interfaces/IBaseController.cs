using System;
using System.Threading.Tasks;

namespace Server.Infrastructure.Entities.Interfaces
{
    public interface IBaseController<TModel>
    {
        Task CreateAsync(TModel entity);
        Task<TModel> ReadById(Guid id);
        Task<object> Read(object filter);
        Task UpdateAsync(TModel entity);
        Task DeleteAsync(Guid id);
    }
}