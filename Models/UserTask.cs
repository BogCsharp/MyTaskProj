using Microsoft.AspNetCore.Identity;

namespace PracApp.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }  
        public IdentityUser User { get; set; }
    }
}
