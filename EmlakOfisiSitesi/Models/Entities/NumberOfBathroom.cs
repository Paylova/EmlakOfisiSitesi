using EmlakOfisiSitesi.Models.Entities.Comman;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class NumberOfBathroom : BaseEntity
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public bool IsAndOver { get; set; }
    }
}
