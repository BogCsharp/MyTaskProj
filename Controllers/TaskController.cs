using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult IndexTasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = _db.Tasks.Where(t => t.UserId == userId).ToList();
            return View(tasks);
        }

        [HttpPost]
        public IActionResult Create(UserTask task)
        {
            task.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _db.Tasks.Add(task);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
