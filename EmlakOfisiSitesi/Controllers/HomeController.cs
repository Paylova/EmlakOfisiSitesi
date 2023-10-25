using EmlakOfisiSitesi.Models;
using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmlakOfisiSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<BuildingAge> _buildingAgeRepository;
        private readonly IRepository<DateOfAdvertisement> _dateOfAdvertisementRepository;
        private readonly IRepository<DeedStatus> _deedStatusRepository;
        private readonly IRepository<Facade> _facadeRepository;
        private readonly IRepository<FloorLocation> _floorLocationRepository;
        private readonly IRepository<HeatingType> _heatingTypeRepository;
        private readonly IRepository<HousingAdvertisement> _housingAdvertisementRepository;
        private readonly IRepository<HousingType> _housingTypeRepository;
        private readonly IRepository<NumberOfBathroom> _numberOfBathroomRepository;
        private readonly IRepository<NumberOfFloorsInBuilding> _numberOfFloorsInBuildingRepository;
        private readonly IRepository<NumberOfRoomHall> _numberOfRoomHallRepository;
        private readonly IRepository<UsageStatus> _usageStatusRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly Models.DbContext _context;

        public HomeController(IRepository<BuildingAge> buildingAgeRepository, IRepository<DateOfAdvertisement> dateOfAdvertisementRepository, IRepository<DeedStatus> deedStatusRepository, IRepository<Facade> facadeRepository, IRepository<FloorLocation> floorLocationRepository, IRepository<HeatingType> heatingTypeRepository, IRepository<HousingAdvertisement> housingAdvertisementRepository, IRepository<HousingType> housingTypeRepository, IRepository<NumberOfBathroom> numberOfBathroomRepository, IRepository<NumberOfRoomHall> numberOfRoomHallRepository, IRepository<UsageStatus> usageStatusRepository, IRepository<NumberOfFloorsInBuilding> numberOfFloorsInBuildingRepository, IRepository<City> cityRepository, IRepository<District> districtRepository, Models.DbContext context)
        {
            _buildingAgeRepository = buildingAgeRepository;
            _dateOfAdvertisementRepository = dateOfAdvertisementRepository;
            _deedStatusRepository = deedStatusRepository;
            _facadeRepository = facadeRepository;
            _floorLocationRepository = floorLocationRepository;
            _heatingTypeRepository = heatingTypeRepository;
            _housingAdvertisementRepository = housingAdvertisementRepository;
            _housingTypeRepository = housingTypeRepository;
            _numberOfBathroomRepository = numberOfBathroomRepository;
            _numberOfRoomHallRepository = numberOfRoomHallRepository;
            _usageStatusRepository = usageStatusRepository;
            _numberOfFloorsInBuildingRepository = numberOfFloorsInBuildingRepository;
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {

            IEnumerable<HousingAdvertisement> housingAdvertisements = _housingAdvertisementRepository.GetAll();

            var viewModel = GetAllFilterList();
            var result = new Tuple<IEnumerable<HousingAdvertisement>, EmlakFiltreViewModel>(housingAdvertisements, viewModel);

            return View(result);
        }

        [HttpPost]
        public IActionResult List(EmlakFiltreViewModel filterDatas)
        {
            IEnumerable<HousingAdvertisement> housingAdvertisements = UseFilter(filterDatas);

            var viewModel = GetAllFilterList();
            var result = new Tuple<IEnumerable<HousingAdvertisement>, EmlakFiltreViewModel>(housingAdvertisements, viewModel);

            return View(result);
        }

        [HttpGet()]
        public IActionResult Detail(Guid id)
        {
            if (id != Guid.Empty)
            {
                HousingAdvertisement housingAdvertisement = _housingAdvertisementRepository.GetById(id);

                return View(housingAdvertisement);
            }

            return View();
        }

        public IEnumerable<HousingAdvertisement> UseFilter(EmlakFiltreViewModel filterDatas)
        {
            var query = _context.HousingAdvertisements
           .Include(a => a.HousingType)
           .Include(a => a.HeatingType)
           .Include(a => a.FloorLocation)
           .Include(a => a.City)
           .Include(a => a.District)
           .Include(a => a.Agent)
           .Include(a => a.HousingAdvertisementPhotos)
           .Include(a => a.DeedStatus)
           .Include(a => a.Facade)
           .Include(a => a.UsageStatus)
           .AsQueryable();

            if (filterDatas.MinPrice > 0 || filterDatas.MaxPrice > 0)
            {
                query = query.Where(a => (filterDatas.MinPrice <= 0 || a.Price >= filterDatas.MinPrice) && (filterDatas.MaxPrice <= 0 || a.Price <= filterDatas.MaxPrice));
            }

            if (filterDatas.MinSquareMeters > 0 || filterDatas.MaxSquareMeters > 0)
            {
                query = query.Where(a => (filterDatas.MinSquareMeters <= 0 || a.GrossSquareMeters >= filterDatas.MinSquareMeters) && (filterDatas.MaxSquareMeters <= 0 || a.GrossSquareMeters <= filterDatas.MaxSquareMeters));
            }

            if (!string.IsNullOrEmpty(filterDatas.SelectedIsSale) && filterDatas.SelectedIsSale != "all")
            {
                bool isForSale = filterDatas.SelectedIsSale == "true";
                query = query.Where(a => a.IsForSale == isForSale);
            }

            if (!string.IsNullOrEmpty(filterDatas.SelectedIsCreditEligibility) && filterDatas.SelectedIsCreditEligibility != "all")
            {
                bool isCreditEligibility = filterDatas.SelectedIsCreditEligibility == "true";
                query = query.Where(a => a.IsCreditEligibility == isCreditEligibility);
            }

            if (!string.IsNullOrEmpty(filterDatas.SelectedIsFurnished) && filterDatas.SelectedIsFurnished != "all")
            {
                bool isFurnished = filterDatas.SelectedIsFurnished == "true";
                query = query.Where(a => a.IsFurnished == isFurnished);
            }

            if (!string.IsNullOrEmpty(filterDatas.SelectedIsOnSite) && filterDatas.SelectedIsOnSite != "all")
            {
                bool isOnSite = filterDatas.SelectedIsOnSite == "true";
                query = query.Where(a => a.IsOnSite == isOnSite);
            }

            if (!string.IsNullOrEmpty(filterDatas.SelectedIsSuitableForTrade) && filterDatas.SelectedIsSuitableForTrade != "all")
            {
                bool isSuitableForTrade = filterDatas.SelectedIsSuitableForTrade == "true";
                query = query.Where(a => a.IsSuitableForTrade == isSuitableForTrade);
            }

            if (filterDatas.FacadeId != Guid.Empty)
            {
                query = query.Where(a => a.Facade.Id == filterDatas.FacadeId);
            }

            if (filterDatas.HeatingTypeId != Guid.Empty)
            {
                query = query.Where(a => a.HeatingType.Id == filterDatas.HeatingTypeId);
            }

            if (filterDatas.HousingTypeId != Guid.Empty)
            {
                query = query.Where(a => a.HousingType.Id == filterDatas.HousingTypeId);
            }

            if (filterDatas.DeedStatusId != Guid.Empty)
            {
                query = query.Where(a => a.DeedStatus != null && a.DeedStatus.Id == filterDatas.DeedStatusId);
            }

            if (filterDatas.FloorLocationId != Guid.Empty)
            {
                query = query.Where(a => a.FloorLocation.Id == filterDatas.FloorLocationId);
            }

            if (filterDatas.UsageStatusId != Guid.Empty)
            {
                query = query.Where(a => a.UsageStatus.Id == filterDatas.UsageStatusId);
            }

            if (filterDatas.CityId != Guid.Empty)
            {
                query = query.Where(a => a.City.Id == filterDatas.CityId);
            }

            if (filterDatas.DistrictId != Guid.Empty)
            {
                query = query.Where(a => a.District.Id == filterDatas.DistrictId);
            }

            if (filterDatas.BuildingAgeId != Guid.Empty)
            {
                var buildingAge = _buildingAgeRepository.GetById(filterDatas.BuildingAgeId);

                if (buildingAge != null)
                {
                    if (buildingAge.IsAndOver)
                    {
                        query = query.Where(a => a.BuildingAge >= buildingAge.Min);
                    }
                    else
                    {
                        query = query.Where(a => a.BuildingAge >= buildingAge.Min && a.BuildingAge <= buildingAge.Max);
                    }
                }
            }

            if (filterDatas.NumberOfFloorsInBuildingId != Guid.Empty)
            {
                var numberOfFloors = _numberOfFloorsInBuildingRepository.GetById(filterDatas.NumberOfFloorsInBuildingId);

                if (numberOfFloors != null)
                {
                    query = query.Where(a => a.FloorNumber >= numberOfFloors.Min && a.FloorNumber <= numberOfFloors.Max);
                }
            }

            if (filterDatas.NumberOfBathroomId != Guid.Empty)
            {
                var numberOfBathrooms = _numberOfBathroomRepository.GetById(filterDatas.NumberOfBathroomId);

                if (numberOfBathrooms != null)
                {
                    if (numberOfBathrooms.IsAndOver)
                    {
                        query = query.Where(a => a.BathroomNumber >= numberOfBathrooms.Number);
                    }
                    else
                    {
                        query = query.Where(a => a.BathroomNumber == numberOfBathrooms.Number);
                    }
                }
            }

            if (filterDatas.NumberOfRoomHallId != Guid.Empty)
            {
                var numberOfRoomHall = _numberOfRoomHallRepository.GetById(filterDatas.NumberOfRoomHallId);

                if (numberOfRoomHall != null)
                {
                    if (numberOfRoomHall.IsAndOver)
                    {
                        query = query.Where(a => a.RoomNumber == numberOfRoomHall.RoomNumber && a.HallNumber >= numberOfRoomHall.HallNumber);
                    }
                    else
                    {
                        query = query.Where(a => a.RoomNumber == numberOfRoomHall.RoomNumber && a.HallNumber == numberOfRoomHall.HallNumber);
                    }
                }
            }

            if (filterDatas.DateOfAdvertisementId != Guid.Empty)
            {
                var dateOfAdvertisement = _dateOfAdvertisementRepository.GetById(filterDatas.DateOfAdvertisementId);

                if (dateOfAdvertisement != null)
                {
                    var targetDate = DateTime.Now.Date.AddDays(-dateOfAdvertisement.Day);
                    query = query.Where(a => a.CreatedDate >= targetDate);
                }
            }

            return query.ToList();
        }

        public EmlakFiltreViewModel GetAllFilterList()
        {
            IEnumerable<BuildingAge> buildingAges = _buildingAgeRepository.GetAll();
            IEnumerable<DateOfAdvertisement> dateOfAdvertisements = _dateOfAdvertisementRepository.GetAll();
            IEnumerable<DeedStatus> deedStatuses = _deedStatusRepository.GetAll();
            IEnumerable<Facade> facades = _facadeRepository.GetAll();
            IEnumerable<FloorLocation> floorLocations = _floorLocationRepository.GetAll();
            IEnumerable<HeatingType> heatingTypes = _heatingTypeRepository.GetAll();
            IEnumerable<HousingType> housingTypes = _housingTypeRepository.GetAll();
            IEnumerable<NumberOfBathroom> numberOfBathrooms = _numberOfBathroomRepository.GetAll();
            IEnumerable<NumberOfFloorsInBuilding> numberOfFloorsInBuildings = _numberOfFloorsInBuildingRepository.GetAll();
            IEnumerable<NumberOfRoomHall> numberOfRoomHalls = _numberOfRoomHallRepository.GetAll();
            IEnumerable<UsageStatus> usageStatuses = _usageStatusRepository.GetAll();
            IEnumerable<City> cities = _cityRepository.GetAll();
            IEnumerable<District> districts = _districtRepository.GetAll();

            EmlakFiltreViewModel viewModel = new EmlakFiltreViewModel
            {
                BuildingAges = buildingAges.Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() }),
                DateOfAdvertisements = dateOfAdvertisements.Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }),
                DeedStatuses = deedStatuses.Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }),
                Facades = facades.Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() }),
                FloorLocations = floorLocations.Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() }),
                HeatingTypes = heatingTypes.Select(h => new SelectListItem { Text = h.Name, Value = h.Id.ToString() }),
                HousingTypes = housingTypes.Select(h => new SelectListItem { Text = h.Name, Value = h.Id.ToString() }),
                NumberOfBathrooms = numberOfBathrooms.Select(n => new SelectListItem { Text = n.Name, Value = n.Id.ToString() }),
                NumberOfFloorsInBuildings = numberOfFloorsInBuildings.Select(n => new SelectListItem { Text = n.Name, Value = n.Id.ToString() }),
                UsageStatuses = usageStatuses.Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                NumberOfRoomHalls = numberOfRoomHalls.Select(n => new SelectListItem { Text = n.Name, Value = n.Id.ToString() }),
                Cities = cities.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
                Districts = districts.Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }),
            };

            return viewModel;
        }
    }
}