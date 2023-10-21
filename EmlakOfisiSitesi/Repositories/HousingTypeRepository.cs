using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class HousingTypeRepository : IRepository<HousingType>
    {
        private readonly Models.DbContext _context;

        public HousingTypeRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(HousingType entity)
        {
            await _context.HousingTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<HousingType> entities)
        {
            await _context.HousingTypes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<HousingType> GetAll()
        {
            return _context.HousingTypes;
        }

        public HousingType GetById(Guid id)
        {
            return _context.HousingTypes.Find(id);
        }

        public async Task Remove(HousingType entity)
        {
            _context.HousingTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<HousingType> entities)
        {
            _context.HousingTypes.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(HousingType entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
