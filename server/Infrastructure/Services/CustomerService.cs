using SecurityWebApp.Infrastructure.Entities.Exceptions;
using SecurityWebApp.Infrastructure.Entities.Filter;
using SecurityWebApp.Infrastructure.Entities.Models;
using SecurityWebApp.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace SecurityWebApp.Infrastructure.Services
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

            pEntity.SetId();
            pEntity.SetCreationDate();
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

        public async Task<object> Read(CustomerFilter pEntity)
        {
            return await _repository.Read(pEntity);
        }

        public async Task UpdateAsync(CustomerModel pEntity)
        {
            CustomerModel entity = await ReadById(pEntity.Id);
            CustomerModel customerValid = await _repository.ReadByMail(pEntity.Mail);

            if (customerValid != null && !(customerValid.Id.Equals(pEntity.Id)))
            {
                throw new BadRequestException("Email already registered") { };
            }

            if (!entity.Equals(pEntity))
            {
                pEntity.SetUpdateDate();
                await _repository.UpdateAsync(pEntity);
            }
        }

        public async Task DeleteAsync(Guid pId)
        {
            CustomerModel entity = await ReadById(pId);            
            await _repository.DeleteAsync(entity);
        }
    }
}
