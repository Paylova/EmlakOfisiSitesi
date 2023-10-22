using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class HousingTypeViewModelValidator : AbstractValidator<HousingTypeViewModel>
    {
        public HousingTypeViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Konut tipi adı boş olamaz.");
        }
    }
}
