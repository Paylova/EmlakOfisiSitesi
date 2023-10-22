using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class NumberOfFloorsInBuildingViewModelValidator : AbstractValidator<NumberOfFloorsInBuildingViewModel>
    {
        public NumberOfFloorsInBuildingViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Bina kat sayısı adı boş olamaz.");
            RuleFor(model => model.Min).GreaterThanOrEqualTo(0).WithMessage("Minimum değer 0'dan büyük veya eşit olmalıdır.");
            RuleFor(model => model.Max).GreaterThan(model => model.Min).WithMessage("Maksimum değer minimum değerden büyük olmalıdır.");
        }
    }
}
