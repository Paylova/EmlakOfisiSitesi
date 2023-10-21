using Microsoft.AspNetCore.Identity;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class Agent : IdentityUser
    {
        public ICollection<HousingAdvertisement> HousingAdvertisements { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
