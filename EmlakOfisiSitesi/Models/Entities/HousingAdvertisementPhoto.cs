using EmlakOfisiSitesi.Models.Entities.Comman;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class HousingAdvertisementPhoto : BaseEntity
    {
        public string FilePath { get; set; }
        public HousingAdvertisement HousingAdvertisement { get; set; }
    }
}
