using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
