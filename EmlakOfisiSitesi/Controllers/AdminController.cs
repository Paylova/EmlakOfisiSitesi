using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
