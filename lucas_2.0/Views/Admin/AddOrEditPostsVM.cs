using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace lucas_2._0.Views.Admin
{
    public class AddOrEditPostsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
       


        public int SubCategoryId { get; set; }


    }
}
