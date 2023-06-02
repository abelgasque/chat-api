using SecurityApp.Web.Infrastructure.Entities.Exceptions;
using SecurityApp.Web.Infrastructure.Entities.Models;
using SecurityApp.Web.Infrastructure.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SecurityApp.Web.Infrastructure.Services
{
    public class CustomerRoleService
    {
        private readonly CustomerRoleRepository _repository;

        public CustomerRoleService(CustomerRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerRoleModel> ReadByCode(string pCode)
        {
            CustomerRoleModel entity = await _repository.ReadByCode(pCode);
            if (entity is null) throw new BadRequestException("Register not found!") { };
            return entity;
        }

        public async Task<List<CustomerRoleModel>> ReadAll()
        {
            return await _repository.ReadAll();
        }

    }
}
