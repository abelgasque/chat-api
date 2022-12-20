using Microsoft.EntityFrameworkCore;
using SecurityApp.Web.Infrastructure.Entities.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityApp.Web.Infrastructure.Repositories
{
    public class CustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerModel> CreateAsync(CustomerModel pEntity)
        {
            _context.Customer.Add(pEntity);
            int nuResult = await _context.SaveChangesAsync();
            return (nuResult > 0) ? pEntity : null;
        }

        public async Task<CustomerModel> ReadById(Guid pId)
        {
            return await _context.Customer.AsNoTracking().Where(e => e.Id == pId).FirstOrDefaultAsync();
        }

        public async Task<object> Read()
        {
            IQueryable<CustomerModel> query = _context.Customer.AsNoTracking();
            return new
            {
                total = query.Select(x => new { x.Id }).Count(),
                data = await query.ToListAsync(),
            };
        }

        public async Task<CustomerModel> UpdateAsync(CustomerModel pEntity)
        {
            _context.Customer.Update(pEntity);
            int nuResult = await _context.SaveChangesAsync();
            return (nuResult > 0) ? pEntity : null;
        }

        public async Task DeleteAsync(CustomerModel pEntity)
        {
            _context.Customer.Remove(pEntity);
            await _context.SaveChangesAsync();
        }
    }
}
