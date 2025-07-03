namespace PracApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserTask> Tasks { get; set; } = new();
    }
}
