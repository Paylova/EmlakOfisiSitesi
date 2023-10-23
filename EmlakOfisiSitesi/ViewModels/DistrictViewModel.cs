using EmlakOfisiSitesi.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmlakOfisiSitesi.ViewModels
{
    public class DistrictViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
        public bool? IsActive { get; set; }
    }
}
