using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracApp.Data;
using PracApp.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;
namespace PracApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaskController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = _db.Tasks
                .Where(t => t.UserId == userId)
                .Include(t => t.Category) 
                .ToList();

            return View(tasks);
        }
        [HttpGet]
        public IActionResult CreateTask()
        {
            ViewBag.Categories = _db.Categories.ToList() ?? new List<Category>();

            return View();
        }
        [HttpPost]
        public IActionResult CreateTask(UserTask task)
        {
            task.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _db.Tasks.Add(task);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        

        [HttpPost]
        public IActionResult DeleteTask(int id)
        {
            var task = _db.Tasks.FirstOrDefault(t => t.Id == id && t.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (task == null)
            {
                return NotFound();
            }

            _db.Tasks.Remove(task);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
