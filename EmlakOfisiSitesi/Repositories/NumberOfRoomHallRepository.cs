using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class NumberOfRoomHallRepository : IRepository<NumberOfRoomHall>
    {
        private readonly Models.DbContext _context;

        public NumberOfRoomHallRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(NumberOfRoomHall entity)
        {
            await _context.NumberOfRoomHalls.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<NumberOfRoomHall> entities)
        {
            await _context.NumberOfRoomHalls.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<NumberOfRoomHall> GetAll(bool? IsActive = null)
        {
            if (IsActive.HasValue)
                return _context.NumberOfRoomHalls.Where(ba => ba.IsActive == IsActive);
            return _context.NumberOfRoomHalls;
        }

        public NumberOfRoomHall GetById(Guid id)
        {
            return _context.NumberOfRoomHalls.Find(id);
        }

        public async Task Remove(NumberOfRoomHall entity)
        {
            _context.NumberOfRoomHalls.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<NumberOfRoomHall> entities)
        {
            _context.NumberOfRoomHalls.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(NumberOfRoomHall entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
