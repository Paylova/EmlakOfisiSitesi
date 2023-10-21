using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class HeatingTypeRepository : IRepository<HeatingType>
    {
        private readonly Models.DbContext _context;

        public HeatingTypeRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(HeatingType entity)
        {
            await _context.HeatingTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<HeatingType> entities)
        {
            await _context.HeatingTypes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<HeatingType> GetAll()
        {
            return _context.HeatingTypes;
        }

        public HeatingType GetById(Guid id)
        {
            return _context.HeatingTypes.Find(id);
        }

        public async Task Remove(HeatingType entity)
        {
            _context.HeatingTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<HeatingType> entities)
        {
            _context.HeatingTypes.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(HeatingType entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
