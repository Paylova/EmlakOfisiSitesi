using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class FacadeRepository : IRepository<Facade>
    {
        private readonly Models.DbContext _context;

        public FacadeRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(Facade entity)
        {
            await _context.Facades.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Facade> entities)
        {
            await _context.Facades.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Facade> GetAll()
        {
            return _context.Facades;
        }

        public Facade GetById(Guid id)
        {
            return _context.Facades.Find(id);
        }

        public async Task Remove(Facade entity)
        {
            _context.Facades.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Facade> entities)
        {
            _context.Facades.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Facade entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
