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
        public async Task<IActionResult> CreateTask()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Categories = await _db.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
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
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _db.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _db.Categories.Where(c=>c.UserId==userId).ToListAsync();
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserTask task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isUserTask = await _db.Tasks.AnyAsync(t => t.Id == id && t.UserId == userId);

            if (!isUserTask)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    task.UserId = userId; 
                    _db.Update(task);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _db.Tasks.AnyAsync(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View(task);
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
            category.UserId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (category == null) return NotFound();

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
