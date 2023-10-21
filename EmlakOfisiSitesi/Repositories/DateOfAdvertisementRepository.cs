using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class DateOfAdvertisementRepository : IRepository<DateOfAdvertisement>
    {
        private readonly Models.DbContext _context;

        public DateOfAdvertisementRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(DateOfAdvertisement entity)
        {
            await _context.DateOfAdvertisements.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<DateOfAdvertisement> entities)
        {
            await _context.DateOfAdvertisements.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<DateOfAdvertisement> GetAll()
        {
            return _context.DateOfAdvertisements;
        }

        public DateOfAdvertisement GetById(Guid id)
        {
            return _context.DateOfAdvertisements.Find(id);
        }

        public async Task Remove(DateOfAdvertisement entity)
        {
            _context.DateOfAdvertisements.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<DateOfAdvertisement> entities)
        {
            _context.DateOfAdvertisements.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(DateOfAdvertisement entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
