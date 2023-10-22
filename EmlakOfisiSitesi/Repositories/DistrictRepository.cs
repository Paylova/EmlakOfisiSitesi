using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class DistrictRepository : IRepository<District>
    {
        private readonly Models.DbContext _context;

        public DistrictRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(District entity)
        {
            await _context.Districts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<District> entities)
        {
            await _context.Districts.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<District> GetAll()
        {
            return _context.Districts;
        }

        public District GetById(Guid id)
        {
            return _context.Districts.Find(id);
        }

        public async Task Remove(District entity)
        {
            _context.Districts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<District> entities)
        {
            _context.Districts.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(District entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
