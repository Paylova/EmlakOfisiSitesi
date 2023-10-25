using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.Services.FileManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    [Authorize]
    public class HousingAdvertisementPhotoController : Controller
    {
        private readonly IRepository<HousingAdvertisementPhoto> _housingAdvertisementPhotoRepository;
        private readonly IRepository<HousingAdvertisement> _housingAdvertisementRepository;
        private readonly IFileManager _fileManager;

        public HousingAdvertisementPhotoController(IRepository<HousingAdvertisementPhoto> housingAdvertisementPhotoRepository, IRepository<HousingAdvertisement> housingAdvertisementRepository, IFileManager fileManager)
        {
            _housingAdvertisementPhotoRepository = housingAdvertisementPhotoRepository;
            _housingAdvertisementRepository = housingAdvertisementRepository;
            _fileManager = fileManager;
        }
        [HttpGet]
        public IActionResult HousingAdvertisementPhotoEdit(Guid Id)
        {
            HousingAdvertisement housingAdvertisement = _housingAdvertisementRepository.GetById(Id);
            if (housingAdvertisement == null) { return NotFound(); }

            List<HousingAdvertisementPhoto> housingAdvertisementPhotos = housingAdvertisement.HousingAdvertisementPhotos.ToList();
            ViewBag.HousingAdvertisementId = housingAdvertisement.Id;
            return View(housingAdvertisementPhotos);
        }
        [HttpPost]
        public IActionResult Delete(Guid id, string path)
        {
            HousingAdvertisementPhoto housingAdvertisementPhoto = _housingAdvertisementPhotoRepository.GetById(id);
            _housingAdvertisementPhotoRepository.Remove(housingAdvertisementPhoto);
            HousingAdvertisementPhoto deletedhousingAdvertisementPhoto = _housingAdvertisementPhotoRepository.GetById(id);
            if (deletedhousingAdvertisementPhoto == null)
            {
                _fileManager.Delete(path, "wwwroot/images/HousingAdvertisements");
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public IActionResult Update(Guid id, string path, IFormFile file)
        {
            try
            {
                HousingAdvertisementPhoto housingAdvertisementPhoto = _housingAdvertisementPhotoRepository.GetById(id);
                if (housingAdvertisementPhoto != null)
                {
                    _fileManager.Delete(path, "wwwroot/images/HousingAdvertisements");
                    string updatedFilePath = _fileManager.Upload(file, "wwwroot/images/HousingAdvertisements");
                    if (!string.IsNullOrEmpty(updatedFilePath))
                    {
                        housingAdvertisementPhoto.FilePath = updatedFilePath;
                        _housingAdvertisementPhotoRepository.Update(housingAdvertisementPhoto);
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = "Dosya yükleme hatası" });
                    }
                }
                else
                {
                    return Json(new { success = false, errorMessage = "Resim bulunamadı." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateOrder(Guid id, int order)
        {
            try
            {
                HousingAdvertisementPhoto housingAdvertisementPhoto = _housingAdvertisementPhotoRepository.GetById(id);
                if (housingAdvertisementPhoto != null)
                {
                    housingAdvertisementPhoto.Order = order;
                    _housingAdvertisementPhotoRepository.Update(housingAdvertisementPhoto);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public IActionResult MakeMainImage(Guid imageId, Guid houseadvertisementId)
        {
            try
            {
                HousingAdvertisementPhoto selectedImage = _housingAdvertisementPhotoRepository.GetById(imageId);
                if (selectedImage == null)
                {
                    return Json(new { success = false, errorMessage = "Seçilen fotoğraf bulunamadı." });
                }

                HousingAdvertisement housingAdvertisement = _housingAdvertisementRepository.GetById(houseadvertisementId);
                if (housingAdvertisement == null)
                {
                    return Json(new { success = false, errorMessage = "Araba bulunamadı." });
                }

                if (selectedImage.Order == 1)
                {
                    return Json(new { success = false, errorMessage = "Seçilen fotoğraf zaten ana fotoğraf." });
                }

                HousingAdvertisementPhoto mainImage = housingAdvertisement.HousingAdvertisementPhotos.FirstOrDefault(image => image.Order == 1);
                if (mainImage != null)
                {
                    mainImage.Order = selectedImage.Order;
                }

                selectedImage.Order = 1;
                housingAdvertisement.MainImageId = selectedImage.Id;
                _housingAdvertisementRepository.Update(housingAdvertisement);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(Guid houseadvertisementId, IFormFile file)
        {
            try
            {
                var housingAdvertisement = _housingAdvertisementRepository.GetById(houseadvertisementId);
                if (housingAdvertisement == null)
                {
                    return Json(new { success = false, errorMessage = "Proje bulunamadı." });
                }

                if (file == null || file.Length == 0)
                {
                    return Json(new { success = false, errorMessage = "Lütfen bir dosya seçin." });
                }

                string uploadDirectory = "wwwroot/images/HousingAdvertisements";
                string uniqueFileName = _fileManager.Upload(file, uploadDirectory);

                HousingAdvertisementPhoto housingAdvertisementPhoto = new HousingAdvertisementPhoto
                {
                    Id = Guid.NewGuid(),
                    HousingAdvertisement = housingAdvertisement,
                    FilePath = uniqueFileName,
                    Order = int.MaxValue,
                    IsMain = false
                };

                await _housingAdvertisementPhotoRepository.Add(housingAdvertisementPhoto);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }
    }
}
