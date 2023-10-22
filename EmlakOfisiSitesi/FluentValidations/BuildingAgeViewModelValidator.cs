using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class BuildingAgeViewModelValidator : AbstractValidator<BuildingAgeViewModel>
    {
        public BuildingAgeViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Ad alanı boş olamaz.");
            RuleFor(model => model.Min).GreaterThanOrEqualTo(0).WithMessage("Minimum değer 0 veya daha büyük olmalıdır.");
            RuleFor(model => model.Max).GreaterThan(model => model.Min).WithMessage("Maksimum değer minimum değerden büyük olmalıdır.");
        }
    }
}
