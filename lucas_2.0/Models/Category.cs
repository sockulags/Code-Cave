using System.ComponentModel.DataAnnotations;

namespace lucas_2._0.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public List<SubCategory>? SubCategories { get; set; }

    }
}
