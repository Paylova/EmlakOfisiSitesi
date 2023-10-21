using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    public class HousingAdvertisementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
