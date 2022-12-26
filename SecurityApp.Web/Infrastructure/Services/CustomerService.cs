using SecurityApp.Web.Infrastructure.Entities.Exceptions;
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

        public async Task CreateAsync(CustomerModel pEntity)
        {
            CustomerModel entity = await _repository.ReadByMail(pEntity.Mail);
            if (!(entity is null))
            {
                throw new BadRequestException("E-mail already registered") { };
            }

            await _repository.CreateAsync(pEntity);
        }

        public async Task<CustomerModel> ReadById(Guid pId)
        {
            CustomerModel entity = await _repository.ReadById(pId);
            if (entity is null) throw new BadRequestException("Register not found!") { };
            return entity;
        }

        public async Task<CustomerModel> ReadByMail(string pMail)
        {
            CustomerModel entity = await _repository.ReadByMail(pMail);
            if (entity is null) throw new BadRequestException("Register not found!") { };            
            return entity;
        }

        public async Task<object> Read(CustomerModel pEntity)
        {
            return await _repository.Read(pEntity);
        }

        public async Task UpdateAsync(CustomerModel pEntity)
        {
            CustomerModel entity = await _repository.ReadById(pEntity.Id);
            CustomerModel customerValid = await _repository.ReadByMail(pEntity.Mail);

            if (customerValid != null && !(customerValid.Id.Equals(pEntity.Id)))
            {
                throw new BadRequestException("E-mail already registered") { };
            }
            if (!entity.Equals(pEntity)) await _repository.UpdateAsync(pEntity);
        }

        public async Task DeleteAsync(Guid pId)
        {
            CustomerModel entity = await ReadById(pId);            
            await _repository.DeleteAsync(entity);
        }
    }
}
