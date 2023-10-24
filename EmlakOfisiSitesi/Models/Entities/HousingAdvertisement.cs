using EmlakOfisiSitesi.Models.Entities.Comman;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class HousingAdvertisement : BaseEntity
    {
        public HousingType HousingType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RoomNumber { get; set; }
        public int HallNumber { get; set; }
        public int BathroomNumber { get; set; }
        public decimal GrossSquareMeters { get; set; }
        public decimal NetSquareMeters { get; set; }
        public HeatingType HeatingType { get; set; }
        public int BuildingAge { get; set; }
        public FloorLocation FloorLocation { get; set; }
        public int FloorNumber { get; set; }
        public bool IsCreditEligibility { get; set; }
        public bool IsFurnished { get; set; }
        public UsageStatus UsageStatus { get; set; }
        public Facade Facade { get; set; }
        public decimal Dues { get; set; }
        public DeedStatus? DeedStatus { get; set; }
        public bool IsSuitableForTrade { get; set; }
        public bool IsOnSite { get; set; }
        public decimal RentalIncome { get; set; }
        public decimal Deposit { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public string Address { get; set; }
        public bool IsForSale { get; set; }
        public bool IsForRent { get; set; }
        public Agent Agent { get; set; }
        public Guid MainImageId { get; set; }
        public ICollection<HousingAdvertisementPhoto> HousingAdvertisementPhotos { get; set; }
    }
}
