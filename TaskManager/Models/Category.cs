namespace TaskManager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Tasks> tasks { get; set; }
    }
}
