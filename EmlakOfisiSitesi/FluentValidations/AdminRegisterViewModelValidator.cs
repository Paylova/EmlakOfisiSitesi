using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class AdminRegisterViewModelValidator : AbstractValidator<AdminRegisterViewModel>
    {
        public AdminRegisterViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("İsim alanı boş olamaz");
            RuleFor(model => model.Surname).NotEmpty().WithMessage("Soyisim alanı boş olamaz");
            RuleFor(model => model.UserName).NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz");
            RuleFor(model => model.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz").EmailAddress().WithMessage("Geçerli bir e-posta adresi girin");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz");
            RuleFor(model => model.ConfirmPassword).Equal(model => model.Password).WithMessage("Şifreler uyuşmuyor");
        }
    }
}
