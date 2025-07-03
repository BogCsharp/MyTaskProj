using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracApp.Models;

namespace PracApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
