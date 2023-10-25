using EmlakOfisiSitesi.Models;
using EmlakOfisiSitesi.Models.Entities;

namespace EmlakOfisiSitesi.Repositories
{
    public class AdminRepository : IRepository<Admin>
    {
        private readonly Models.DbContext _context;

        public AdminRepository(DbContext context)
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
            throw new NotImplementedException();
        }

        public Task Remove(Admin entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<Admin> entities)
        {
            throw new NotImplementedException();
        }

        public Task Update(Admin entity)
        {
            throw new NotImplementedException();
        }
    }
}
