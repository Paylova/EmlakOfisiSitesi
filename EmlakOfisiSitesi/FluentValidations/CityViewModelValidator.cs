using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class CityViewModelValidator : AbstractValidator<CityViewModel>
    {
        public CityViewModelValidator()
        {
            RuleFor(city => city.Name)
                .NotEmpty().WithMessage("Şehir adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Şehir adı en fazla 50 karakter olmalıdır.");
        }
    }
}
