using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PracApp.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public IsComplited IsComplited { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? UserId { get; set; }  
        public IdentityUser? User { get; set; }
    }
    public enum IsComplited
    {
        [Display(Name = "Не начата")]
        NotStarted,
        [Display(Name = "В работе")]
        InProgress,
        [Display(Name = "Завершена")]
        Completed,
        [Display(Name = "Отменена")]
        Cancelled

    }
}
