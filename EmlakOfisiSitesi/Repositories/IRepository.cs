namespace EmlakOfisiSitesi.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task Remove(TEntity entity);
        Task RemoveRange(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
    }
}
