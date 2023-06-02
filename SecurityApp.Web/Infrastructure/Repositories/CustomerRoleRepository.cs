using Microsoft.EntityFrameworkCore;
using SecurityApp.Web.Infrastructure.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityApp.Web.Infrastructure.Repositories
{
    public class CustomerRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerRoleModel>> ReadAll()
        {
            return await _context.CustomerRole.AsNoTracking().Where(e => e.Active.Equals(true)).ToListAsync();
        }

        public async Task<CustomerRoleModel> ReadByCode(string pCode)
        {
            return await _context.CustomerRole.AsNoTracking().Where(e => e.Code.Equals(pCode)).FirstOrDefaultAsync(e => e.Active.Equals(true));
        }
    }
}
