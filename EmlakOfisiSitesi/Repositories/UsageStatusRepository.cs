using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class UsageStatusRepository : IRepository<UsageStatus>
    {
        private readonly Models.DbContext _context;

        public UsageStatusRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(UsageStatus entity)
        {
            await _context.UsageStatuses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<UsageStatus> entities)
        {
            await _context.UsageStatuses.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<UsageStatus> GetAll(bool? IsActive = null)
        {
            if (IsActive.HasValue)
                return _context.UsageStatuses.Where(ba => ba.IsActive == IsActive);
            return _context.UsageStatuses;
        }

        public UsageStatus GetById(Guid id)
        {
            return _context.UsageStatuses.Find(id);
        }

        public async Task Remove(UsageStatus entity)
        {
            _context.UsageStatuses.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<UsageStatus> entities)
        {
            _context.UsageStatuses.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UsageStatus entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
