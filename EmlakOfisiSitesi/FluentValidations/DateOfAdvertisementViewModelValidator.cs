using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class DateOfAdvertisementViewModelValidator : AbstractValidator<DateOfAdvertisementViewModel>
    {
        public DateOfAdvertisementViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Ad alanı boş olamaz.");
            RuleFor(model => model.Day).NotEmpty().WithMessage("Gün sayısı 0'dan büyük olmalıdır.");
        }
    }
}
