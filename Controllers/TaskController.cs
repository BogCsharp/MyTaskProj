using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracApp.Data;
using PracApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;
namespace PracApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaskController(ApplicationDbContext db) => _db = db;

        public async Task <IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = await _db.Tasks
                .Where(t => t.UserId == userId)
                .Include(t => t.Category) 
                .ToListAsync();

            return View(tasks);
        }
        [HttpGet]
        public async Task <IActionResult> CreateTask()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync() ?? new List<Category>();

            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> CreateTask(UserTask task)
        {
            task.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        

        [HttpPost]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task =await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (task == null)
            {
                return NotFound();
            }

            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public  IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
