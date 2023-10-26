using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class AgentRepository : IRepository<Agent>
    {
        private readonly Models.DbContext _context;

        public AgentRepository(Models.DbContext context)
        {
            _context = context;
        }

        public Task Add(Agent entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<Agent> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Agent> GetAll(bool? IsActive = null)
        {
            return _context.Agents.ToList();
        }

        public Agent GetById(Guid id)
        {
            return _context.Agents.Find(id.ToString());
        }

        public async Task Remove(Agent entity)
        {
            _context.Agents.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task RemoveRange(IEnumerable<Agent> entities)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Agent entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
