using Microsoft.AspNetCore.Identity;

namespace PracApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserTask> Tasks { get; set; } = new();
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
