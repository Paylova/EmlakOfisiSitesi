using EmlakOfisiSitesi.FluentValidations;
using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmlakOfisiSitesi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
            builder.Services.AddDbContext<Models.DbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("localhost")
                ));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<Models.DbContext>().AddDefaultTokenProviders();

            builder.Services.AddMvc().AddSessionStateTempDataProvider();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            builder.Services.AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Error/Unauthorized";
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });
            builder.Services.AddScoped<IRepository<BuildingAge>, BuildingAgeRepository>();
            builder.Services.AddScoped<IRepository<DateOfAdvertisement>, DateOfAdvertisementRepository>();
            builder.Services.AddScoped<IRepository<DeedStatus>, DeedStatusRepository>();
            builder.Services.AddScoped<IRepository<Facade>, FacadeRepository>();
            builder.Services.AddScoped<IRepository<FloorLocation>, FloorLocationRepository>();
            builder.Services.AddScoped<IRepository<HeatingType>, HeatingTypeRepository>();
            builder.Services.AddScoped<IRepository<HousingAdvertisementPhoto>, HousingAdvertisementPhotoRepository>();
            builder.Services.AddScoped<IRepository<HousingAdvertisement>, HousingAdvertisementRepository>();
            builder.Services.AddScoped<IRepository<HousingType>, HousingTypeRepository>();
            builder.Services.AddScoped<IRepository<NumberOfBathroom>, NumberOfBathroomRepository>();
            builder.Services.AddScoped<IRepository<NumberOfFloorsInBuilding>, NumberOfFloorsInBuildingRepository>();
            builder.Services.AddScoped<IRepository<NumberOfRoomHall>, NumberOfRoomHallRepository>();
            builder.Services.AddScoped<IRepository<UsageStatus>, UsageStatusRepository>();


            //builder.Services.AddScoped<IValidator<BuildingAgeViewModel>,BuildingAgeViewModelValidator>();
            //builder.Services.AddScoped < IValidator<DateOfAdvertisementViewModel>

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            try
            {
                var scope = app.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<Models.DbContext>();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ctx.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if (!ctx.Roles.Any())
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    var adminUser = new IdentityUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "admin",
                        Email = "adminemlak@hotmail.com",
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };
                    var result = await userMgr.CreateAsync(adminUser, "parolaadmin123");
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            app.UseCookiePolicy();
            app.Run();
        }
    }
}