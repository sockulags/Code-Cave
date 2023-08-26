using System.ComponentModel.DataAnnotations;

namespace lucas_2._0.Models
{
    public class Post
    {

        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }
        public string Code { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;


        //Connections to the correct SubCategory
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}