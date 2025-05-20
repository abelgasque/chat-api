using Microsoft.EntityFrameworkCore;
using SecurityWebApp.Infrastructure.Entities.DTO;
using SecurityWebApp.Infrastructure.Entities.Filter;
using SecurityWebApp.Infrastructure.Entities.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Infrastructure.Repositories
{
    public class CustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CustomerModel pEntity)
        {
            _context.Customer.Add(pEntity);
            if (!(await _context.SaveChangesAsync() > 0))
            {
                throw new Exception("There was an error create record");
            }
        }

        public async Task<CustomerModel> ReadById(Guid pId)
        {
            return await _context.Customer.AsNoTracking().Where(e => e.Id.Equals(pId)).FirstOrDefaultAsync();
        }

        public async Task<CustomerModel> ReadByMail(string pMail)
        {
            return await _context.Customer.AsNoTracking().Where(e => e.Mail.Equals(pMail)).FirstOrDefaultAsync();
        }

        public async Task<object> Read(CustomerFilter pEntity)
        {
            IQueryable<CustomerModel> query = _context.Customer.AsNoTracking();
            if (pEntity.Id != null) query = query.Where(e => e.Id.Equals(pEntity.Id));
            if (!string.IsNullOrEmpty(pEntity.FirstName)) query = query.Where(e => e.FirstName.Contains(pEntity.FirstName));
            if (!string.IsNullOrEmpty(pEntity.LastName)) query = query.Where(e => e.LastName.Contains(pEntity.LastName));
            if (!string.IsNullOrEmpty(pEntity.Mail)) query = query.Where(e => e.Mail.Contains(pEntity.Mail));
            if (pEntity.Active != null) query = query.Where(e => e.Active.Equals(pEntity.Active));
            if (pEntity.Block != null) query = query.Where(e => e.Block.Equals(pEntity.Block));

            if (pEntity.CreationDateStart.HasValue && pEntity.CreationDateEnd.HasValue)
            {
                DateTime dtStartRange = pEntity.CreationDateStart.Value;
                DateTime dtEndRange = pEntity.CreationDateEnd.Value;
                query = query.Where(e =>
                                e.CreationDate >= new DateTime(dtStartRange.Year, dtStartRange.Month, dtStartRange.Day)
                                && e.CreationDate <= new DateTime(dtEndRange.Year, dtEndRange.Month, dtEndRange.Day, 23, 59, 59));
            }

            if (pEntity.UpdateDateStart.HasValue && pEntity.UpdateDateEnd.HasValue)
            {
                DateTime dtStartRange = pEntity.UpdateDateStart.Value;
                DateTime dtEndRange = pEntity.UpdateDateEnd.Value;
                query = query.Where(e =>
                                e.UpdateDate >= new DateTime(dtStartRange.Year, dtStartRange.Month, dtStartRange.Day)
                                && e.UpdateDate <= new DateTime(dtEndRange.Year, dtEndRange.Month, dtEndRange.Day, 23, 59, 59));
            }

            int count = query.Select(x => new { x.Id }).Count();
            query = query.OrderByDescending(e => e.CreationDate).Skip((pEntity.Page - 1) * pEntity.Size).Take(pEntity.Size);

            return new PaginationResponseDTO
            {
                Page = pEntity.Page,
                Size = pEntity.Size,
                Total = count,
                Data = await query.Select(e => new CustomerDTO(e) { }).ToListAsync(),
            };
        }

        public async Task UpdateAsync(CustomerModel pEntity)
        {
            _context.Customer.Update(pEntity);
            if (!(await _context.SaveChangesAsync() > 0))
            {
                throw new Exception("There was an error update record");
            }
        }

        public async Task DeleteAsync(CustomerModel pEntity)
        {
            _context.Customer.Remove(pEntity);
            await _context.SaveChangesAsync();
        }
    }
}
