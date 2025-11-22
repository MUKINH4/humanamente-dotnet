namespace humanamente.Models
{

    public class Profession
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}