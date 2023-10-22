using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class NumberOfFloorsInBuildingRepository : IRepository<NumberOfFloorsInBuilding>
    {
        private readonly Models.DbContext _context;

        public NumberOfFloorsInBuildingRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(NumberOfFloorsInBuilding entity)
        {
            await _context.NumberOfFloorsInBuildings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<NumberOfFloorsInBuilding> entities)
        {
            await _context.NumberOfFloorsInBuildings.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<NumberOfFloorsInBuilding> GetAll()
        {
            return _context.NumberOfFloorsInBuildings;
        }

        public NumberOfFloorsInBuilding GetById(Guid id)
        {
            return _context.NumberOfFloorsInBuildings.Find(id);
        }

        public async Task Remove(NumberOfFloorsInBuilding entity)
        {
            _context.NumberOfFloorsInBuildings.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<NumberOfFloorsInBuilding> entities)
        {
            _context.NumberOfFloorsInBuildings.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(NumberOfFloorsInBuilding entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
