using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class HousingAdvertisementRepository : IRepository<HousingAdvertisement>
    {
        private readonly Models.DbContext _context;

        public HousingAdvertisementRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(HousingAdvertisement entity)
        {
            await _context.HousingAdvertisements.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<HousingAdvertisement> entities)
        {
            await _context.HousingAdvertisements.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<HousingAdvertisement> GetAll()
        {
            return _context.HousingAdvertisements;
        }

        public HousingAdvertisement GetById(Guid id)
        {
            return _context.HousingAdvertisements.Find(id);
        }

        public async Task Remove(HousingAdvertisement entity)
        {
            _context.HousingAdvertisements.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<HousingAdvertisement> entities)
        {
            _context.HousingAdvertisements.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(HousingAdvertisement entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
