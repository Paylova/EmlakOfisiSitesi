using EmlakOfisiSitesi.Models;
using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class AdminRepository : IRepository<Admin>
    {
        private readonly Models.DbContext _context;

        public AdminRepository(Models.DbContext context)
        {
            _context = context;
        }

        public Task Add(Admin entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<Admin> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }

        public Admin GetById(Guid id)
        {
            return _context.Admins.Find(id.ToString());
        }

        public async Task Remove(Admin entity)
        {
            _context.Admins.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task RemoveRange(IEnumerable<Admin> entities)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Admin entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
