using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.Services.FileManager;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace EmlakOfisiSitesi.Controllers
{
    [Authorize]
    public class HousingAdvertisementController : Controller
    {
        private readonly IRepository<HousingType> _housingTypeRepository;
        private readonly IRepository<HeatingType> _heatingTypeRepository;
        private readonly IRepository<FloorLocation> _floorLocationRepository;
        private readonly IRepository<UsageStatus> _usageStatusRepository;
        private readonly IRepository<Facade> _facadeRepository;
        private readonly IRepository<DeedStatus> _deedStatusRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<HousingAdvertisement> _housingAdvertisementRepository;
        private readonly IHousingAdvertisementRepository<HousingAdvertisement> _privateHousingAdvertisementRepository;
        private readonly IRepository<HousingAdvertisementPhoto> _housingAdvertisementPhoto;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IFileManager _fileManager;
        private readonly IValidator<HousingAdvertisementViewModel> _housingAdvertisementValidator;
        private readonly IValidator<EditHousingAdvertisementViewModel> _editHousingAdvertisementValidator;

        public HousingAdvertisementController(IRepository<HousingType> housingTypeRepository, IRepository<HeatingType> heatingTypeRepository, IRepository<FloorLocation> floorLocationRepository, IRepository<UsageStatus> usageStatusRepository, IRepository<Facade> facadeRepository, IRepository<DeedStatus> deedStatusRepository, IRepository<District> districtRepository, IRepository<HousingAdvertisement> housingAdvertisement, IRepository<HousingAdvertisementPhoto> housingAdvertisementPhoto, UserManager<IdentityUser> userManager, IFileManager fileManager, IValidator<HousingAdvertisementViewModel> housingAdvertisementValidator, IRepository<City> cityRepository, IHousingAdvertisementRepository<HousingAdvertisement> privateHousingAdvertisementRepository, IValidator<EditHousingAdvertisementViewModel> editHousingAdvertisementValidator)
        {
            _housingTypeRepository = housingTypeRepository;
            _heatingTypeRepository = heatingTypeRepository;
            _floorLocationRepository = floorLocationRepository;
            _usageStatusRepository = usageStatusRepository;
            _facadeRepository = facadeRepository;
            _deedStatusRepository = deedStatusRepository;
            _districtRepository = districtRepository;
            _housingAdvertisementRepository = housingAdvertisement;
            _housingAdvertisementPhoto = housingAdvertisementPhoto;
            _userManager = userManager;
            _fileManager = fileManager;
            _housingAdvertisementValidator = housingAdvertisementValidator;
            _cityRepository = cityRepository;
            _privateHousingAdvertisementRepository = privateHousingAdvertisementRepository;
            _editHousingAdvertisementValidator = editHousingAdvertisementValidator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var userId = Request.Cookies["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles.Contains("Admin"))
                    {
                        IEnumerable<HousingAdvertisement> housingAdvertisements = _housingAdvertisementRepository.GetAll();
                        return View(housingAdvertisements);
                    }
                    else if (userRoles.Contains("Agent"))
                    {
                        user = await _userManager.FindByIdAsync(userId);
                        Agent agentUser = (Agent)await _userManager.FindByIdAsync(userId);
                        IEnumerable<HousingAdvertisement> housingAdvertisements = _privateHousingAdvertisementRepository.GetHousingAdvertisementsWithUserId(Guid.Parse(agentUser.Id));
                        return View(housingAdvertisements);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Agent")]
        public IActionResult CreateSale()
        {
            var housingTypes = _housingTypeRepository.GetAll();
            var heatingTypes = _heatingTypeRepository.GetAll();
            var floorLocations = _floorLocationRepository.GetAll();
            var usageStatues = _usageStatusRepository.GetAll();
            var facadeTypes = _facadeRepository.GetAll();
            var deedStatues = _deedStatusRepository.GetAll();
            var districts = _districtRepository.GetAll();
            var cities = _cityRepository.GetAll();

            var housingAdvertisementViewModel = new HousingAdvertisementViewModel
            {
                HousingTypes = housingTypes.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                HeatingTypes = heatingTypes.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                FloorLocations = floorLocations.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                UsageStatues = usageStatues.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Facades = facadeTypes.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                DeedStatues = deedStatues.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Districts = districts.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),

            };
            return View(housingAdvertisementViewModel);
        }

        [HttpPost]
        //[Authorize(Policy = "Agent")]
        public async Task<IActionResult> CreateSale(HousingAdvertisementViewModel housingAdvertisementViewModel)
        {
            var validationResult = await _housingAdvertisementValidator.ValidateAsync(housingAdvertisementViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                GetAllTypes(housingAdvertisementViewModel);
                var deedStatues = _deedStatusRepository.GetAll();
                housingAdvertisementViewModel.DeedStatues = deedStatues.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                return View(housingAdvertisementViewModel);
            }
            HousingType housingType = _housingTypeRepository.GetById(housingAdvertisementViewModel.HousingTypeId);
            HeatingType heatingType = _heatingTypeRepository.GetById(housingAdvertisementViewModel.HeatingTypeId);
            FloorLocation floorLocation = _floorLocationRepository.GetById(housingAdvertisementViewModel.FloorLocationId);
            UsageStatus usageStatus = _usageStatusRepository.GetById(housingAdvertisementViewModel.UsageStatusId);
            Facade facade = _facadeRepository.GetById(housingAdvertisementViewModel.FacadeId);
            DeedStatus deedStatus = _deedStatusRepository.GetById(housingAdvertisementViewModel.DeedStatusId);
            City city = _cityRepository.GetById(housingAdvertisementViewModel.CityId);
            District district = _districtRepository.GetById(housingAdvertisementViewModel.DistrictId);
            var userId = Request.Cookies["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                IdentityUser user = await _userManager.FindByIdAsync(userId);
                Agent agentUser = (Agent)await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    HousingAdvertisement housingAdvertisement = new HousingAdvertisement
                    {
                        HousingType = housingType,
                        Title = housingAdvertisementViewModel.Title,
                        Description = housingAdvertisementViewModel.Description,
                        Price = housingAdvertisementViewModel.Price,
                        RoomNumber = housingAdvertisementViewModel.RoomNumber,
                        HallNumber = housingAdvertisementViewModel.HallNumber,
                        BathroomNumber = housingAdvertisementViewModel.BathroomNumber,
                        GrossSquareMeters = housingAdvertisementViewModel.GrossSquareMeters,
                        NetSquareMeters = housingAdvertisementViewModel.NetSquareMeters,
                        HeatingType = heatingType,
                        BuildingAge = housingAdvertisementViewModel.BuildingAge,
                        FloorLocation = floorLocation,
                        FloorNumber = housingAdvertisementViewModel.FloorNumber,
                        IsCreditEligibility = housingAdvertisementViewModel.IsCreditEligibility,
                        IsFurnished = housingAdvertisementViewModel.IsFurnished,
                        UsageStatus = usageStatus,
                        Facade = facade,
                        Dues = housingAdvertisementViewModel.Dues,
                        DeedStatus = deedStatus,
                        IsSuitableForTrade = housingAdvertisementViewModel.IsSuitableForTrade,
                        IsOnSite = housingAdvertisementViewModel.IsOnSite,
                        RentalIncome = housingAdvertisementViewModel.RentalIncome,
                        City = city,
                        District = district,
                        Address = housingAdvertisementViewModel.Address,
                        IsForSale = true,
                        IsForRent = false,
                        Agent = agentUser,
                        IsActive = true
                    };
                    string uploadDirectory = "wwwroot/images/HousingAdvertisements";
                    List<HousingAdvertisementPhoto> housingAdvertisementPhotos = new List<HousingAdvertisementPhoto>();
                    bool isFirstImage = true;
                    foreach (var image in housingAdvertisementViewModel.HousingAdvertisementPhotos)
                    {
                        string uniqueFileName = _fileManager.Upload(image, uploadDirectory);
                        if (!string.IsNullOrEmpty(uniqueFileName))
                        {
                            HousingAdvertisementPhoto housingAdvertisementPhoto = new HousingAdvertisementPhoto
                            {
                                Id = Guid.NewGuid(),
                                FilePath = uniqueFileName,
                                Order = isFirstImage ? 0 : int.MaxValue
                            };

                            housingAdvertisementPhotos.Add(housingAdvertisementPhoto);

                            if (isFirstImage)
                            {
                                housingAdvertisement.MainImageId = housingAdvertisementPhoto.Id;
                                housingAdvertisementPhoto.Order = 1;
                                housingAdvertisementPhoto.IsMain = true;
                                isFirstImage = false;
                            }
                        }
                    }
                    housingAdvertisement.HousingAdvertisementPhotos = housingAdvertisementPhotos;
                    await _housingAdvertisementRepository.Add(housingAdvertisement);
                    return RedirectToAction("Index", "Admin");
                }
            }

            return View();
        }


        [HttpGet]
        //[Authorize(Policy = "Agent")]
        public IActionResult CreateRent()
        {
            var housingTypes = _housingTypeRepository.GetAll();
            var heatingTypes = _heatingTypeRepository.GetAll();
            var floorLocations = _floorLocationRepository.GetAll();
            var usageStatues = _usageStatusRepository.GetAll();
            var facadeTypes = _facadeRepository.GetAll();
            var districts = _districtRepository.GetAll();
            var cities = _cityRepository.GetAll();

            var housingAdvertisementViewModel = new HousingAdvertisementViewModel
            {
                HousingTypes = housingTypes.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                HeatingTypes = heatingTypes.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                FloorLocations = floorLocations.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                UsageStatues = usageStatues.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Facades = facadeTypes.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Districts = districts.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(housingAdvertisementViewModel);
        }


        [HttpPost]
        //[Authorize(Policy = "Agent")]
        public async Task<IActionResult> CreateRent(HousingAdvertisementViewModel housingAdvertisementViewModel)
        {
            var validationResult = await _housingAdvertisementValidator.ValidateAsync(housingAdvertisementViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                GetAllTypes(housingAdvertisementViewModel);

                return View(housingAdvertisementViewModel);
            }
            HousingType housingType = _housingTypeRepository.GetById(housingAdvertisementViewModel.HousingTypeId);
            HeatingType heatingType = _heatingTypeRepository.GetById(housingAdvertisementViewModel.HeatingTypeId);
            FloorLocation floorLocation = _floorLocationRepository.GetById(housingAdvertisementViewModel.FloorLocationId);
            UsageStatus usageStatus = _usageStatusRepository.GetById(housingAdvertisementViewModel.UsageStatusId);
            Facade facade = _facadeRepository.GetById(housingAdvertisementViewModel.FacadeId);
            City city = _cityRepository.GetById(housingAdvertisementViewModel.CityId);
            District district = _districtRepository.GetById(housingAdvertisementViewModel.DistrictId);

            var userId = Request.Cookies["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                IdentityUser user = await _userManager.FindByIdAsync(userId);
                Agent agentUser = (Agent)await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    HousingAdvertisement housingAdvertisement = new HousingAdvertisement
                    {
                        HousingType = housingType,
                        Title = housingAdvertisementViewModel.Title,
                        Description = housingAdvertisementViewModel.Description,
                        Price = housingAdvertisementViewModel.Price,
                        RoomNumber = housingAdvertisementViewModel.RoomNumber,
                        HallNumber = housingAdvertisementViewModel.HallNumber,
                        BathroomNumber = housingAdvertisementViewModel.BathroomNumber,
                        GrossSquareMeters = housingAdvertisementViewModel.GrossSquareMeters,
                        NetSquareMeters = housingAdvertisementViewModel.NetSquareMeters,
                        HeatingType = heatingType,
                        BuildingAge = housingAdvertisementViewModel.BuildingAge,
                        FloorLocation = floorLocation,
                        FloorNumber = housingAdvertisementViewModel.FloorNumber,
                        IsFurnished = housingAdvertisementViewModel.IsFurnished,
                        UsageStatus = usageStatus,
                        Facade = facade,
                        Dues = housingAdvertisementViewModel.Dues,
                        IsOnSite = housingAdvertisementViewModel.IsOnSite,
                        Deposit = housingAdvertisementViewModel.Deposit,
                        City = city,
                        District = district,
                        DeedStatus = null,
                        Address = housingAdvertisementViewModel.Address,
                        IsForSale = false,
                        IsForRent = true,
                        Agent = agentUser,
                        IsActive = true
                    };

                    string uploadDirectory = "wwwroot/images/HousingAdvertisements";
                    List<HousingAdvertisementPhoto> housingAdvertisementPhotos = new List<HousingAdvertisementPhoto>();
                    bool isFirstImage = true;
                    foreach (var image in housingAdvertisementViewModel.HousingAdvertisementPhotos)
                    {
                        string uniqueFileName = _fileManager.Upload(image, uploadDirectory);
                        if (!string.IsNullOrEmpty(uniqueFileName))
                        {
                            HousingAdvertisementPhoto housingAdvertisementPhoto = new HousingAdvertisementPhoto
                            {
                                Id = Guid.NewGuid(),
                                FilePath = uniqueFileName,
                                Order = isFirstImage ? 0 : int.MaxValue
                            };

                            housingAdvertisementPhotos.Add(housingAdvertisementPhoto);

                            if (isFirstImage)
                            {
                                housingAdvertisement.MainImageId = housingAdvertisementPhoto.Id;
                                housingAdvertisementPhoto.Order = 1;
                                housingAdvertisementPhoto.IsMain = true;
                                isFirstImage = false;
                            }
                        }
                    }
                    housingAdvertisement.HousingAdvertisementPhotos = housingAdvertisementPhotos;
                    await _housingAdvertisementRepository.Add(housingAdvertisement);
                    return RedirectToAction("List", "HousingAdvertisement");
                }
            }
            return View();
        }

        [HttpGet]
        //[Authorize(Policy = "Agent")]
        public IActionResult Update(Guid id)
        {
            HousingAdvertisement housingAdvertisement = _housingAdvertisementRepository.GetById(id);
            if (housingAdvertisement == null) { return NotFound(); }
            var deedStatues = _deedStatusRepository.GetAll();
            var districts = _districtRepository.GetAll();
            var facades = _facadeRepository.GetAll();
            var floorLocations = _floorLocationRepository.GetAll();
            var heatingTypes = _heatingTypeRepository.GetAll();
            var housingTypes = _housingTypeRepository.GetAll();
            var cities = _cityRepository.GetAll();
            var usageStatuses = _usageStatusRepository.GetAll();


            EditHousingAdvertisementViewModel editHousingAdvertisementViewModel = new EditHousingAdvertisementViewModel
            {
                Id = housingAdvertisement.Id,
                Address = housingAdvertisement.Address,
                Title = housingAdvertisement.Title,
                BuildingAge = housingAdvertisement.BuildingAge,
                BathroomNumber = housingAdvertisement.BathroomNumber,
                Deposit = housingAdvertisement.Deposit,
                Description = housingAdvertisement.Description,
                Dues = housingAdvertisement.Dues,
                FloorNumber = housingAdvertisement.FloorNumber,
                HallNumber = housingAdvertisement.HallNumber,
                GrossSquareMeters = housingAdvertisement.GrossSquareMeters,
                NetSquareMeters = housingAdvertisement.NetSquareMeters,
                IsCreditEligibility = housingAdvertisement.IsCreditEligibility,
                IsFurnished = housingAdvertisement.IsFurnished,
                IsOnSite = housingAdvertisement.IsOnSite,
                IsSuitableForTrade = housingAdvertisement.IsSuitableForTrade,
                Price = housingAdvertisement.Price,
                RentalIncome = housingAdvertisement.RentalIncome,
                RoomNumber = housingAdvertisement.RoomNumber,
                DeedStatusId = housingAdvertisement.DeedStatus.Id,
                DeedStatues = deedStatues.Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }),
                CityId = housingAdvertisement.City.Id,
                Cities = cities.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
                DistrictId = housingAdvertisement.District.Id,
                Districts = districts.Select(d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }),
                FacadeId = housingAdvertisement.Facade.Id,
                Facades = facades.Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() }),
                FloorLocationId = housingAdvertisement.FloorLocation.Id,
                FloorLocations = floorLocations.Select(f => new SelectListItem { Text = f.Name, Value = f.Id.ToString() }),
                HeatingTypeId = housingAdvertisement.HeatingType.Id,
                HeatingTypes = heatingTypes.Select(h => new SelectListItem { Text = h.Name, Value = h.Id.ToString() }),
                HousingTypeId = housingAdvertisement.HousingType.Id,
                HousingTypes = housingTypes.Select(h => new SelectListItem { Text = h.Name, Value = h.Id.ToString() }),
                UsageStatusId = housingAdvertisement.UsageStatus.Id,
                UsageStatues = usageStatuses.Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
            };
            return View(editHousingAdvertisementViewModel);
        }

        [HttpPost]
        //[Authorize(Policy = "Agent")]
        public async Task<IActionResult> Update(EditHousingAdvertisementViewModel housingAdvertisementViewModel)
        {
            var validationResult = await _editHousingAdvertisementValidator.ValidateAsync(housingAdvertisementViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);
                var housingTypes = _housingTypeRepository.GetAll();
                var heatingTypes = _heatingTypeRepository.GetAll();
                var floorLocations = _floorLocationRepository.GetAll();
                var usageStatues = _usageStatusRepository.GetAll();
                var facadeTypes = _facadeRepository.GetAll();
                var cities = _cityRepository.GetAll();
                var districts = _districtRepository.GetAll();
                var deedStatues = _deedStatusRepository.GetAll();

                housingAdvertisementViewModel.HousingTypes = housingTypes.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                housingAdvertisementViewModel.HeatingTypes = heatingTypes.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                housingAdvertisementViewModel.FloorLocations = floorLocations.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                housingAdvertisementViewModel.UsageStatues = usageStatues.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                housingAdvertisementViewModel.Facades = facadeTypes.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                housingAdvertisementViewModel.Cities = cities.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                housingAdvertisementViewModel.Districts = districts.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                housingAdvertisementViewModel.DeedStatues = deedStatues.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                return View(housingAdvertisementViewModel);
            }
            HousingAdvertisement housingAdvertisement = _housingAdvertisementRepository.GetById(housingAdvertisementViewModel.Id);
            HousingType housingType = _housingTypeRepository.GetById(housingAdvertisementViewModel.HousingTypeId);
            HeatingType heatingType = _heatingTypeRepository.GetById(housingAdvertisementViewModel.HeatingTypeId);
            FloorLocation floorLocation = _floorLocationRepository.GetById(housingAdvertisementViewModel.FloorLocationId);
            UsageStatus usageStatus = _usageStatusRepository.GetById(housingAdvertisementViewModel.UsageStatusId);
            Facade facade = _facadeRepository.GetById(housingAdvertisementViewModel.FacadeId);
            City city = _cityRepository.GetById(housingAdvertisementViewModel.CityId);
            District district = _districtRepository.GetById(housingAdvertisementViewModel.DistrictId);
            DeedStatus deedStatus = _deedStatusRepository.GetById(housingAdvertisementViewModel.DeedStatusId);

            housingAdvertisement.Address = housingAdvertisementViewModel.Address;
            housingAdvertisement.Title = housingAdvertisementViewModel.Title;
            housingAdvertisement.BuildingAge = housingAdvertisementViewModel.BuildingAge;
            housingAdvertisement.BathroomNumber = housingAdvertisementViewModel.BathroomNumber;
            housingAdvertisement.Deposit = housingAdvertisementViewModel.Deposit;
            housingAdvertisement.Description = housingAdvertisementViewModel.Description;
            housingAdvertisement.Dues = housingAdvertisementViewModel.Dues;
            housingAdvertisement.FloorNumber = housingAdvertisementViewModel.FloorNumber;
            housingAdvertisement.HallNumber = housingAdvertisementViewModel.HallNumber;
            housingAdvertisement.GrossSquareMeters = housingAdvertisementViewModel.GrossSquareMeters;
            housingAdvertisement.NetSquareMeters = housingAdvertisementViewModel.NetSquareMeters;
            housingAdvertisement.IsCreditEligibility = housingAdvertisementViewModel.IsCreditEligibility;
            housingAdvertisement.IsFurnished = housingAdvertisementViewModel.IsFurnished;
            housingAdvertisement.IsOnSite = housingAdvertisementViewModel.IsOnSite;
            housingAdvertisement.IsSuitableForTrade = housingAdvertisementViewModel.IsSuitableForTrade;
            housingAdvertisement.Price = housingAdvertisementViewModel.Price;
            housingAdvertisement.RentalIncome = housingAdvertisementViewModel.RentalIncome;
            housingAdvertisement.RoomNumber = housingAdvertisementViewModel.RoomNumber;

            await _housingAdvertisementRepository.Update(housingAdvertisement);

            return RedirectToAction("List", "HousingAdvertisement");
        }

        [HttpPost]
        //[Authorize(Policy = "Agent")]
        public async Task<IActionResult> Delete(Guid id)
        {
            HousingAdvertisement housingAdvertisement = _housingAdvertisementRepository.GetById(id);

            if (housingAdvertisement == null)
                return NotFound();

            await _housingAdvertisementRepository.Remove(housingAdvertisement);

            return Ok();
        }

        private void GetAllTypes(HousingAdvertisementViewModel housingAdvertisementViewModel)
        {
            var housingTypes = _housingTypeRepository.GetAll();
            var heatingTypes = _heatingTypeRepository.GetAll();
            var floorLocations = _floorLocationRepository.GetAll();
            var usageStatues = _usageStatusRepository.GetAll();
            var facadeTypes = _facadeRepository.GetAll();
            var cities = _cityRepository.GetAll();
            var districts = _districtRepository.GetAll();

            housingAdvertisementViewModel.HousingTypes = housingTypes.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            housingAdvertisementViewModel.HeatingTypes = heatingTypes.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            housingAdvertisementViewModel.FloorLocations = floorLocations.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            housingAdvertisementViewModel.UsageStatues = usageStatues.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            housingAdvertisementViewModel.Facades = facadeTypes.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            housingAdvertisementViewModel.Cities = cities.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            housingAdvertisementViewModel.Districts = districts.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
        }
    }
}
