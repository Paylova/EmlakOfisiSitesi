using EmlakOfisiSitesi.Models;
using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using EmlakOfisiSitesi.ViewModels.TupleViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace EmlakOfisiSitesi.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<BuildingAge> _buildingAgeRepository;
        private IRepository<DateOfAdvertisement> _dateOfAdvertisementRepository;
        private IRepository<DeedStatus> _deedStatusRepository;
        private IRepository<Facade> _facadeRepository;
        private IRepository<FloorLocation> _floorLocationRepository;
        private IRepository<HeatingType> _heatingTypeRepository;
        private IRepository<HousingAdvertisement> _housingAdvertisementRepository;
        private IRepository<HousingType> _housingTypeRepository;
        private IRepository<NumberOfBathroom> _numberOfBathroomRepository;
        private IRepository<NumberOfFloorsInBuilding> _numberOfFloorsInBuildingRepository;
        private IRepository<NumberOfRoomHall> _numberOfRoomHallRepository;
        private IRepository<UsageStatus> _usageStatusRepository;

        public HomeController(IRepository<BuildingAge> buildingAgeRepository, IRepository<DateOfAdvertisement> dateOfAdvertisementRepository, IRepository<DeedStatus> deedStatusRepository, IRepository<Facade> facadeRepository, IRepository<FloorLocation> floorLocationRepository, IRepository<HeatingType> heatingTypeRepository, IRepository<HousingAdvertisement> housingAdvertisementRepository, IRepository<HousingType> housingTypeRepository, IRepository<NumberOfBathroom> numberOfBathroomRepository, IRepository<NumberOfRoomHall> numberOfRoomHallRepository, IRepository<UsageStatus> usageStatusRepository, IRepository<NumberOfFloorsInBuilding> numberOfFloorsInBuildingRepository)
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
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<HousingAdvertisement> housingAdvertisements = _housingAdvertisementRepository.GetAll();
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

            EmlakFiltreViewModel viewModel = new EmlakFiltreViewModel
            {
                BuildingAges = buildingAges,
                DateOfAdvertisements = dateOfAdvertisements,
                DeedStatuses = deedStatuses,
                Fades = facades,
                FloorLocations = floorLocations,
                HeatingTypes = heatingTypes,
                HousingTypes = housingTypes,
                NumberOfBathrooms = numberOfBathrooms,
                HousingAdvertisements = housingAdvertisements,
                NumberOfFloorsInBuildings = numberOfFloorsInBuildings,
                UsageStatuses = usageStatuses,
                NumberOfRoomHalls = numberOfRoomHalls,
            };
            return View(viewModel);
        }
    }
}