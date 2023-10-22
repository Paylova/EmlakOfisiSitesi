using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmlakOfisiSitesi.ViewModels.TupleViewModel
{
    public class EmlakFiltreViewModel
    {
        public List<HousingAdvertisementViewModel >HousingAdvertisementViewModel { get; set; }

        public string SelectedHousingTypeViewModel { get; set; }
        public IEnumerable<SelectListItem> HousingTypeList { get; set; }

        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public int MinSquareMeters { get; set; }
        public int MaxSquareMeters { get; set; }

        public IEnumerable<SelectListItem> NumberOfRoomHallList { get; set; }
        public List<NumberOfRoomHallViewModel> SelectedNumberOfRoomHallViewModel { get; set; }

        public IEnumerable<SelectListItem> NumberOfBathroomList { get; set; }
        public List<NumberOfBathroomViewModel> SelectedNumberOfBathroomViewModel { get; set; }

        public IEnumerable<SelectListItem> BuildingAgeList { get; set; }
        public List<BuildingAgeViewModel> SelectedBuildingAgeViewModel { get; set; }

        public IEnumerable<SelectListItem> FloorLocationList { get; set; }
        public List<FloorLocationViewModel> SelectedFloorLocationViewModel { get; set; }

        public IEnumerable<SelectListItem> NumberOfFloorsInBuildingList { get; set; }
        public List<NumberOfFloorsInBuildingViewModel> SelectedNumberOfFloorsInBuildingViewModel { get; set; }

        public bool SelectedIsCreditEligibility { get; set; }
        public IEnumerable<SelectListItem> IsCreditEligibilityList { get; set; }

        public IEnumerable<SelectListItem> DeedStatusList { get; set; }
        public List<DeedStatusViewModel> SelectedDeedStatusViewModel { get; set; }

        public IEnumerable<SelectListItem> UsageStatusList { get; set; }
        public List<UsageStatusViewModel> SelectedUsageStatusViewModel { get; set; }

        public IEnumerable<SelectListItem> HeatingTypeList { get; set; }
        public List<HeatingTypeViewModel> SelectedHeatingTypeViewModel { get; set; }

        public bool SelectedIsFurnished { get; set; }
        public IEnumerable<SelectListItem> IsFurnishedList { get; set; }

        public bool SelectedIsOnSite { get; set; }
        public IEnumerable<SelectListItem> IsOnSiteList { get; set; }
    }
}
