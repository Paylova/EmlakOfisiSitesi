using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class NumberOfBathroomRepository : IRepository<NumberOfBathroom>
    {
        private readonly Models.DbContext _context;

        public NumberOfBathroomRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(NumberOfBathroom entity)
        {
            await _context.NumberOfBathrooms.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<NumberOfBathroom> entities)
        {
            await _context.NumberOfBathrooms.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<NumberOfBathroom> GetAll()
        {
            return _context.NumberOfBathrooms;
        }

        public NumberOfBathroom GetById(Guid id)
        {
            return _context.NumberOfBathrooms.Find(id);
        }

        public async Task Remove(NumberOfBathroom entity)
        {
            _context.NumberOfBathrooms.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<NumberOfBathroom> entities)
        {
            _context.NumberOfBathrooms.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(NumberOfBathroom entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
