using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class FloorLocationRepository : IRepository<FloorLocation>
    {
        private readonly Models.DbContext _context;

        public FloorLocationRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(FloorLocation entity)
        {
            await _context.FloorLocations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<FloorLocation> entities)
        {
            await _context.FloorLocations.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<FloorLocation> GetAll()
        {
            return _context.FloorLocations;
        }

        public FloorLocation GetById(Guid id)
        {
            return _context.FloorLocations.Find(id);
        }

        public async Task Remove(FloorLocation entity)
        {
            _context.FloorLocations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<FloorLocation> entities)
        {
            _context.FloorLocations.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FloorLocation entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
