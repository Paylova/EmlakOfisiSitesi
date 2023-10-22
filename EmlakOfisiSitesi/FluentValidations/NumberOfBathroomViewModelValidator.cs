using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class NumberOfBathroomViewModelValidator : AbstractValidator<NumberOfBathroomViewModel>
    {
        public NumberOfBathroomViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Banyo sayısı adı boş olamaz.");
            RuleFor(model => model.Number).GreaterThan(0).WithMessage("Banyo sayısı 0'dan büyük olmalıdır.");
        }
    }
}
