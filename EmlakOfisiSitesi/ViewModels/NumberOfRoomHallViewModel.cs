namespace EmlakOfisiSitesi.ViewModels
{
    public class NumberOfRoomHallViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int RoomNumber { get; set; }
        public int HallNumber { get; set; }
        public bool IsAndOver { get; set; }
        public bool? IsActive { get; set; }
    }
}
