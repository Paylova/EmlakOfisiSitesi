using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class HousingAdvertisementRepository : IRepository<HousingAdvertisement>, IHousingAdvertisementRepository<HousingAdvertisement>
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
        public IEnumerable<HousingAdvertisement> GetAll(bool? IsActive = null)
        {
            if (IsActive.HasValue)
                return _context.HousingAdvertisements.Where(x => x.IsActive == IsActive)
           .Include(ha => ha.Agent)
           .Include(ha => ha.City)
           .Include(ha => ha.DeedStatus)
           .Include(ha => ha.Facade)
           .Include(ha => ha.FloorLocation)
           .Include(ha => ha.HeatingType)
           .Include(ha => ha.HousingType)
           .Include(ha => ha.UsageStatus)
           .Include(ha => ha.HousingAdvertisementPhotos)
           .Include(ha => ha.District)
           .Include(ha => ha.DeedStatus);
            return _context.HousingAdvertisements
           .Include(ha => ha.Agent)
           .Include(ha => ha.City)
           .Include(ha => ha.DeedStatus)
           .Include(ha => ha.Facade)
           .Include(ha => ha.FloorLocation)
           .Include(ha => ha.HeatingType)
           .Include(ha => ha.HousingType)
           .Include(ha => ha.UsageStatus)
           .Include(ha => ha.HousingAdvertisementPhotos)
           .Include(ha => ha.DeedStatus)
           .Include(ha => ha.District);
        }

        public HousingAdvertisement GetById(Guid id)
        {
            return _context.HousingAdvertisements
                .Include(ha => ha.Agent)
                .Include(ha => ha.City)
                .Include(ha => ha.DeedStatus)
                .Include(ha => ha.Facade)
                .Include(ha => ha.FloorLocation)
                .Include(ha => ha.HeatingType)
                .Include(ha => ha.HousingType)
                .Include(ha => ha.UsageStatus)
                .Include(ha => ha.HousingAdvertisementPhotos)
                .Include(ha => ha.District).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<HousingAdvertisement> GetHousingAdvertisementsWithUserId(Guid Id)
        {
            return _context.HousingAdvertisements
                .Include(ha => ha.Agent)
                .Include(ha => ha.City)
                .Include(ha => ha.DeedStatus)
                .Include(ha => ha.Facade)
                .Include(ha => ha.FloorLocation)
                .Include(ha => ha.HeatingType)
                .Include(ha => ha.HousingType)
                .Include(ha => ha.UsageStatus)
                .Include(ha => ha.HousingAdvertisementPhotos)
                .Include(ha => ha.District).Where(x => x.Agent.Id == Id.ToString());
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
