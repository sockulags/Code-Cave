using lucas_2._0.Models;

namespace lucas_2._0.Views.Admin
{
    public class PostsVM
    {
        public List<Post> Posts { get; set; }
        public string SubCategoryName { get; set; }
        public int SubCategoryId { get; set; }
    }
}
