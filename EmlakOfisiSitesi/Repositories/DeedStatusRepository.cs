using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class DeedStatusRepository : IRepository<DeedStatus>
    {
        private readonly Models.DbContext _context;

        public DeedStatusRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(DeedStatus entity)
        {
            await _context.DeedStatuses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<DeedStatus> entities)
        {
            await _context.DeedStatuses.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<DeedStatus> GetAll()
        {
            return _context.DeedStatuses;
        }

        public DeedStatus GetById(Guid id)
        {
            return _context.DeedStatuses.Find(id);
        }

        public async Task Remove(DeedStatus entity)
        {
            _context.DeedStatuses.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<DeedStatus> entities)
        {
            _context.DeedStatuses.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DeedStatus entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
