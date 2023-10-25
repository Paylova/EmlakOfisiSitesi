using EmlakOfisiSitesi.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmlakOfisiSitesi.ViewModels
{
    public class EmlakFiltreViewModel
    {
        public IEnumerable<Agent> Agents { get; set; } = Enumerable.Empty<Agent>();
        public Guid BuildingAgeId { get; set; }
        public IEnumerable<SelectListItem> BuildingAges { get; set; } = new List<SelectListItem>();
        public Guid DateOfAdvertisementId { get; set; }
        public IEnumerable<SelectListItem> DateOfAdvertisements { get; set; } = new List<SelectListItem>();
        public Guid DeedStatusId { get; set; }
        public IEnumerable<SelectListItem> DeedStatuses { get; set; } = new List<SelectListItem>();
        public Guid FacadeId { get; set; }
        public IEnumerable<SelectListItem> Facades { get; set; } = new List<SelectListItem>();
        public Guid FloorLocationId { get; set; }
        public IEnumerable<SelectListItem> FloorLocations { get; set; } = new List<SelectListItem>();
        public Guid HeatingTypeId { get; set; }
        public IEnumerable<SelectListItem> HeatingTypes { get; set; } = new List<SelectListItem>();
        public Guid HousingTypeId { get; set; }
        public IEnumerable<SelectListItem> HousingTypes { get; set; } = new List<SelectListItem>();
        public Guid NumberOfBathroomId { get; set; }
        public IEnumerable<SelectListItem> NumberOfBathrooms { get; set; } = new List<SelectListItem>();
        public Guid NumberOfFloorsInBuildingId { get; set; }
        public IEnumerable<SelectListItem> NumberOfFloorsInBuildings { get; set; } = new List<SelectListItem>();
        public Guid NumberOfRoomHallId { get; set; }
        public IEnumerable<SelectListItem> NumberOfRoomHalls { get; set; } = new List<SelectListItem>();
        public Guid UsageStatusId { get; set; }
        public IEnumerable<SelectListItem> UsageStatuses { get; set; } = new List<SelectListItem>();
        public Guid CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
        public Guid DistrictId { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; } = new List<SelectListItem>();

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public decimal MinSquareMeters { get; set; }
        public decimal MaxSquareMeters { get; set; }

        public string SelectedIsSale { get; set; }

        public string SelectedIsCreditEligibility { get; set; }

        public string SelectedIsFurnished { get; set; }

        public string SelectedIsOnSite { get; set; }

        public string SelectedIsSuitableForTrade { get; set; }

    }
}
