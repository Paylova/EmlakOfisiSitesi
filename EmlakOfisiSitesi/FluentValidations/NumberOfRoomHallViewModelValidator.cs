using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class NumberOfRoomHallViewModelValidator : AbstractValidator<NumberOfRoomHallViewModel>
    {
        public NumberOfRoomHallViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Oda ve salon adı boş olamaz.");
            RuleFor(model => model.RoomNumber).NotEmpty().WithMessage("Oda sayısı girin.");
            //RuleFor(model => model.HallNumber).NotEmpty().WithMessage("salon sayısı girin.");
        }
    }
}
