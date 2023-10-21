using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    public class FacadeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
