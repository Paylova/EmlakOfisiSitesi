using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(model => model.CompanyName)
                .NotEmpty().WithMessage("Şirket adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Şirket adı en fazla 50 karakter olmalıdır.");

            RuleFor(model => model.Name)
                .NotEmpty().WithMessage("İsim boş bırakılamaz.")
                .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olmalıdır.");

            RuleFor(model => model.Surname)
                .NotEmpty().WithMessage("Soyisim boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Soyisim en fazla 50 karakter olmalıdır.");

            RuleFor(model => model.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olmalıdır.");

            RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage("Telefon Numaranız gereklidir.");

            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("E-posta boş bırakılamaz.")
                .MaximumLength(256).WithMessage("E-posta en fazla 256 karakter olmalıdır.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }
    }
}