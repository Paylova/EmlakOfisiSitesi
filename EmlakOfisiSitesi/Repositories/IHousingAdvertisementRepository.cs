namespace EmlakOfisiSitesi.Repositories
{
    public interface IHousingAdvertisementRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetHousingAdvertisementsWithUserId(Guid Id);
    }
}
