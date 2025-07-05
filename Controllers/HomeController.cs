using Microsoft.AspNetCore.Mvc;
using PracApp.Data;
using PracApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PracApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;


        public HomeController(ApplicationDbContext db) => _db = db;
        

        public IActionResult Index()
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
