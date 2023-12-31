﻿using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class EditHousingAdvertisementViewModelValidator : AbstractValidator<EditHousingAdvertisementViewModel>
    {
        public EditHousingAdvertisementViewModelValidator()
        {
            RuleFor(model => model.Title)
                .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olmalıdır.");

            RuleFor(model => model.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz.");

            RuleFor(model => model.Price)
                .GreaterThan(0).WithMessage("Geçerli bir fiyat giriniz.");

            RuleFor(model => model.RoomNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir oda sayısı giriniz.");

            RuleFor(model => model.HallNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir salon sayısı giriniz.");

            RuleFor(model => model.BathroomNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir banyo sayısı giriniz.");

            RuleFor(model => model.GrossSquareMeters)
                .GreaterThan(0).WithMessage("Geçerli bir brüt metrekare giriniz.");

            RuleFor(model => model.NetSquareMeters)
                .GreaterThan(0).WithMessage("Geçerli bir net metrekare giriniz.");

            RuleFor(model => model.Address)
                .NotEmpty().WithMessage("Adres boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Adres en fazla 100 karakter olmalıdır.");
            RuleFor(model => model.HousingTypeId)
           .NotEmpty().WithMessage("Konut tipi seçmelisiniz.");

            RuleFor(model => model.HeatingTypeId)
                .NotEmpty().WithMessage("Isıtma tipi seçmelisiniz.");

            RuleFor(model => model.BuildingAge)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir bina yaşı giriniz.");

            RuleFor(model => model.FloorLocationId)
                .NotEmpty().WithMessage("Kat konumu seçmelisiniz.");

            RuleFor(model => model.FloorNumber)
                .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir kat numarası giriniz.");

            RuleFor(model => model.DistrictId)
                .NotEmpty().WithMessage("İlçe seçmelisiniz.");


        }
    }
}
