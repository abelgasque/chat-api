using SecurityApp.Web.Infrastructure.Entities.Models;
using SecurityApp.Web.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace SecurityApp.Web.Infrastructure.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repository;
        
        public CustomerService(CustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerModel> CreateAsync(CustomerModel pEntity)
        {            
            return await _repository.CreateAsync(pEntity);
        }

        public async Task<CustomerModel> ReadById(Guid pId)
        {
            return await _repository.ReadById(pId);
        }

        public async Task<object> Read()
        {
            return await _repository.Read();
        }

        public async Task<CustomerModel> UpdateAsync(CustomerModel pEntity)
        {
            return await _repository.UpdateAsync(pEntity);
        }

        public async Task DeleteAsync(Guid pId)
        {
            CustomerModel entity = await ReadById(pId);
            if (!(entity is null))
            {
                await _repository.DeleteAsync(entity);
            }
        }
    }
}
