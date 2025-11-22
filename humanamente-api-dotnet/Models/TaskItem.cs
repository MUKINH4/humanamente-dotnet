
namespace humanamente.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Classification { get; set; } = "pending";

        public int ProfessionId { get; set; }

        public Profession Profession { get; set; } = null!;
    }
}