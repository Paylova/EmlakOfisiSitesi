using EmlakOfisiSitesi.Models.Entities.Comman;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class BuildingAge : BaseEntity
    {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
