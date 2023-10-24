using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class HousingAdvertisementViewModelValidator : AbstractValidator<HousingAdvertisementViewModel>
    {
        public HousingAdvertisementViewModelValidator()
        {
            RuleFor(model => model.Title)
                .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olmalıdır.");

            RuleFor(model => model.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz.");

            RuleFor(model => model.Price)
                .GreaterThan(0).WithMessage("Geçerli bir fiyat giriniz.");

            RuleFor(model => model.RoomNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir oda sayısı giriniz.");

            RuleFor(model => model.HallNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir salon sayısı giriniz.");

            RuleFor(model => model.BathroomNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir banyo sayısı giriniz.");

            RuleFor(model => model.GrossSquareMeters)
                .GreaterThan(0).WithMessage("Geçerli bir brüt metrekare giriniz.");

            RuleFor(model => model.NetSquareMeters)
                .GreaterThan(0).WithMessage("Geçerli bir net metrekare giriniz.");

            RuleFor(model => model.Dues)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir aidat miktarı giriniz.");

            RuleFor(model => model.RentalIncome)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir kira geliri giriniz.");

            RuleFor(model => model.Deposit)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir depozito miktarı giriniz.");

            RuleFor(model => model.Address)
                .NotEmpty().WithMessage("Adres boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Adres en fazla 100 karakter olmalıdır.");
            RuleFor(model => model.HousingTypeId)
           .NotEmpty().WithMessage("Konut tipi seçmelisiniz.");

            RuleFor(model => model.HeatingTypeId)
                .NotEmpty().WithMessage("Isıtma tipi seçmelisiniz.");

            RuleFor(model => model.BuildingAge)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir bina yaşı giriniz.");

            RuleFor(model => model.FloorLocationId)
                .NotEmpty().WithMessage("Kat konumu seçmelisiniz.");

            RuleFor(model => model.FloorNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir kat numarası giriniz.");

            RuleFor(model => model.UsageStatusId)
                .NotEmpty().WithMessage("Kullanım durumu seçmelisiniz.");

            RuleFor(model => model.FacadeId)
                .NotEmpty().WithMessage("Cephe tipi seçmelisiniz.");

            RuleFor(model => model.DistrictId)
                .NotEmpty().WithMessage("İlçe seçmelisiniz.");

            RuleFor(model => model.IsSuitableForTrade)
                .NotNull().WithMessage("Ticaret için uygunluk durumunu seçmelisiniz.");

            RuleFor(model => model.IsOnSite)
                .NotNull().WithMessage("Site içinde bulunma durumunu seçmelisiniz.");

            RuleFor(model => model.IsForSale)
                .NotNull().WithMessage("Satılık durumunu seçmelisiniz.");

            RuleFor(model => model.IsForRent)
                .NotNull().WithMessage("Kiralık durumunu seçmelisiniz.");

            RuleFor(c => c.HousingAdvertisementPhotos)
            .NotEmpty().WithMessage("En az bir fotoğraf yükleyin.")
            .Must(BeValidImages).WithMessage("Geçerli fotoğraf dosyaları yükleyin (jpg, jpeg, png, webp).");
        }
        private bool BeValidImages(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return true;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            foreach (var file in files)
            {
                if (file != null)
                {
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                        return false;
                }
            }

            return true;
        }
    }
}
