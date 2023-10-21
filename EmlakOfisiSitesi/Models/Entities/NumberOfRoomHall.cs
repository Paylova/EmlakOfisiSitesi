using EmlakOfisiSitesi.Models.Entities.Comman;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class NumberOfRoomHall : BaseEntity
    {
        public string Name { get; set; }
        public int RoomNumber { get; set; }
        public int HallNumber { get; set; }
        public bool IsAndOver { get; set; }
    }
}
