using EmlakOfisiSitesi.Models;
using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.ViewModels;
using EmlakOfisiSitesi.ViewModels.TupleViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace EmlakOfisiSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmlakOfisiSitesi.Models.DbContext _context;

        public HomeController(ILogger<HomeController> logger, DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}