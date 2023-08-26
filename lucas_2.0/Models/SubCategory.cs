using System.ComponentModel.DataAnnotations;

namespace lucas_2._0.Models
{
    public class SubCategory
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Post> Posts { get; set; }
    }
}