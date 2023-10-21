using EmlakOfisiSitesi.Models.Entities.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracing = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracing = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracing = true);
        Task<T> GetByIdAsync(string id, bool tracing = true);
    }
}
