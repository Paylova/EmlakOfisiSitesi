using EmlakOfisiSitesi.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmlakOfisiSitesi.ViewModels
{
    public class HousingAdvertisementViewModel
    {
        public Guid Id { get; set; }
        public Guid HousingTypeId { get; set; }
        public IEnumerable<SelectListItem> HousingTypes { get; set; } = new List<SelectListItem>();
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RoomNumber { get; set; }
        public int HallNumber { get; set; }
        public int BathroomNumber { get; set; }
        public decimal GrossSquareMeters { get; set; }
        public decimal NetSquareMeters { get; set; }
        public Guid HeatingTypeId { get; set; }
        public IEnumerable<SelectListItem> HeatingTypes { get; set; } = new List<SelectListItem>();
        public int BuildingAge { get; set; }
        public Guid FloorLocationId { get; set; }
        public IEnumerable<SelectListItem> FloorLocations { get; set; } = new List<SelectListItem>();
        public int FloorNumber { get; set; }
        public bool IsCreditEligibility { get; set; }
        public bool? IsFurnished { get; set; }
        public Guid? UsageStatusId { get; set; }
        public IEnumerable<SelectListItem> UsageStatues { get; set; } = new List<SelectListItem>();
        public Guid? FacadeId { get; set; }
        public IEnumerable<SelectListItem> Facades { get; set; } = new List<SelectListItem>();
        public decimal? Dues { get; set; }
        public Guid? DeedStatusId { get; set; }
        public IEnumerable<SelectListItem> DeedStatues { get; set; } = new List<SelectListItem>();
        public bool? IsSuitableForTrade { get; set; }
        public bool? IsOnSite { get; set; }
        public decimal? RentalIncome { get; set; }
        public decimal? Deposit { get; set; }
        public Guid CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
        public Guid DistrictId { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; } = new List<SelectListItem>();
        public string Address { get; set; }
        public bool IsForSale { get; set; }
        public bool IsForRent { get; set; }
        public List<IFormFile> HousingAdvertisementPhotos { get; set; }
        public bool? IsActive { get; set; }
    }
}
