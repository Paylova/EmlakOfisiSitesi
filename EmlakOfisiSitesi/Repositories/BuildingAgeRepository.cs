﻿using EmlakOfisiSitesi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Repositories
{
    public class BuildingAgeRepository : IRepository<BuildingAge>
    {
        private readonly Models.DbContext _context;

        public BuildingAgeRepository(Models.DbContext context)
        {
            _context = context;
        }

        public async Task Add(BuildingAge entity)
        {
            await _context.BuildingAges.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<BuildingAge> entities)
        {
            await _context.BuildingAges.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<BuildingAge> GetAll()
        {
            return _context.BuildingAges;
        }

        public BuildingAge GetById(Guid id)
        {
            return _context.BuildingAges.Find(id);
        }

        public async Task Remove(BuildingAge entity)
        {
            _context.BuildingAges.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<BuildingAge> entities)
        {
            _context.BuildingAges.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(BuildingAge entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
