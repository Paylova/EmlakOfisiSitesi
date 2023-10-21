using EmlakOfisiSitesi.Models.Entities.Comman;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class NumberOfFloorsInBuilding : BaseEntity
    {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
