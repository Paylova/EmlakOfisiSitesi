using EmlakOfisiSitesi.Models.Entities;

namespace EmlakOfisiSitesi.ViewModels
{
    public class AgentViewModel
    {
        public Guid Id { get; set; }
        public ICollection<HousingAdvertisement> HousingAdvertisements { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
