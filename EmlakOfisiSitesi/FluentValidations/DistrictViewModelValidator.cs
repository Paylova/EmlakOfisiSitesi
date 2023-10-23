using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class DistrictViewModelValidator : AbstractValidator<DistrictViewModel>
    {
        public DistrictViewModelValidator()
        {
            RuleFor(district => district.Name)
                .NotEmpty().WithMessage("Bölge adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Bölge adı en fazla 50 karakter olmalıdır.");


            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("Şehir seçilmelidir.");
        }
    }
}
