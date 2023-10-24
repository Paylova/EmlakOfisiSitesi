using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class AgentRepository : IRepository<Agent>
    {
        private readonly DbContext _context;

        public AgentRepository(DbContext context)
        {
            _context = context;
        }

        public async Task Add(Agent entity)
        {
            await _context.Set<Agent>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Agent> entities)
        {
            await _context.Set<Agent>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Agent> GetAll()
        {
            return _context.Set<Agent>().ToList();
        }

        public Agent GetById(Guid id)
        {
            return _context.Set<Agent>().Find(id);
        }

        public async Task Remove(Agent entity)
        {
            _context.Set<Agent>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<Agent> entities)
        {
            _context.Set<Agent>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Agent entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
