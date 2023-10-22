using EmlakOfisiSitesi.Models.Entities.Comman;

namespace EmlakOfisiSitesi.Models.Entities
{
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public City City { get; set; }
    }
}
