using Microsoft.EntityFrameworkCore;

namespace lucas_2._0.Models
{
    public class MenuList
    {
        public readonly ApplicationContext _context;

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<SubCategory> SubCategories { get; set; }

        public MenuList(ApplicationContext context)
        {
            _context = context;
        }

        public MenuList()
        {
        }

        public List<MenuList> MenuListItems()
        {
            List<SubCategory> list = new List<SubCategory>();
            list = _context.SubCategories.ToList();
            
            
            var model =  _context.Categories.Include(p => p.SubCategories).Select(p => new MenuList(_context)
            {
                CategoryId = p.Id,
                CategoryName = p.Name,
                SubCategories = list,
            });
            return model.ToList();
        }


    }
}
