using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Models.Entities.Comman;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmlakOfisiSitesi.Models
{
    public class DbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<DeedStatus> DeedStatuses { get; set; }
        public DbSet<Facade> Facades { get; set; }
        public DbSet<FloorLocation> FloorLocations { get; set; }
        public DbSet<HeatingType> HeatingTypes { get; set; }
        public DbSet<HousingAdvertisement> HousingAdvertisements { get; set; }
        public DbSet<HousingType> HousingTypes { get; set; }
        public DbSet<UsageStatus> UsageStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NumberOfRoomHall> NumberOfRoomHalls { get; set; }
        public DbSet<NumberOfBathroom> NumberOfBathrooms { get; set; }
        public DbSet<BuildingAge> BuildingAges { get; set; }
        public DbSet<NumberOfFloorsInBuilding> NumberOfFloorsInBuildings { get; set; }
        public DbSet<DateOfAdvertisement> DateOfAdvertisements { get; set; }
        public DbSet<HousingAdvertisementPhoto> HousingAdvertisementPhotos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);

        }
    }
}
