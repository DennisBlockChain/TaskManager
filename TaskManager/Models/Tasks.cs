namespace TaskManager.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public Boolean Repetitive { get; set; }
        public Boolean Done { get; set; }
        public DateTime DateDone { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
