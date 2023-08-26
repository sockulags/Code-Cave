using lucas_2._0.Models;

namespace lucas_2._0.Views.Admin
{
    public class SubCategoryVM
    {
        public string CategoryName { get; set; } 

        public int CategoryId { get; set; }

       public List<SubCategory> SubCategories { get; set; }
    }
}
