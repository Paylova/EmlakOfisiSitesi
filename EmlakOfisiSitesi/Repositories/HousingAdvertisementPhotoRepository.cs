using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class HousingAdvertisementPhotoRepository : IRepository<HousingAdvertisementPhoto>
    {
        private readonly Models.DbContext _context;

        public HousingAdvertisementPhotoRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(HousingAdvertisementPhoto entity)
        {
            await _context.HousingAdvertisementPhotos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<HousingAdvertisementPhoto> entities)
        {
            await _context.HousingAdvertisementPhotos.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<HousingAdvertisementPhoto> GetAll(bool? IsActive = null)
        {
            if (IsActive.HasValue)
                return _context.HousingAdvertisementPhotos.Where(ba => ba.IsActive == true);
            return _context.HousingAdvertisementPhotos;
        }

        public HousingAdvertisementPhoto GetById(Guid id)
        {
            return _context.HousingAdvertisementPhotos.Find(id);
        }

        public async Task Remove(HousingAdvertisementPhoto entity)
        {
            _context.HousingAdvertisementPhotos.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<HousingAdvertisementPhoto> entities)
        {
            _context.HousingAdvertisementPhotos.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(HousingAdvertisementPhoto entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
