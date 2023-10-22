using EmlakOfisiSitesi.Models.Entities;

namespace EmlakOfisiSitesi.ViewModels
{
    public class HousingAdvertisementPhotoViewModel
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public HousingAdvertisement HousingAdvertisement { get; set; }
        public bool? IsActive { get; set; }
    }
}
