using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class CityRepository : IRepository<City>
    {
        private readonly Models.DbContext _context;

        public CityRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(City entity)
        {
            await _context.Cities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<City> entities)
        {
            await _context.Cities.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<City> GetAll()
        {
            return _context.Cities;
        }

        public City GetById(Guid id)
        {
            return _context.Cities.Find(id);
        }

        public async Task Remove(City entity)
        {
            _context.Cities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<City> entities)
        {
            _context.Cities.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(City entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
