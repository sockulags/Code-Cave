using lucas_2._0.Views.Admin;
using Microsoft.EntityFrameworkCore;

namespace lucas_2._0.Models
{
    public class DataService
    {
        public readonly ApplicationContext _context;
        public DataService(ApplicationContext context) {
            _context = context;
        }

        public async Task<IndexVM[]> IndexView()
        {
            var post = await _context.Categories.Include(p => p.SubCategories).ToListAsync();

            var model = new IndexVM[post.Count];
           for(int i = 0; i < post.Count; i++)
            {
                model[i] = new IndexVM
                {
                    CategoryName = post[i].Name,
                    CategoryId = post[i].Id,
                    SubCategoryName = post[i].SubCategories.ToString()
                    

                };

            }
            

            return model;
        }

        public AddOrEditCategoryVM GetCategory(int? id)
        {
            var post = _context.Categories.Find(id);

            return new AddOrEditCategoryVM
            {
                CategoryId = post.Id,
                CategoryName = post.Name,
            };
        }

        public async Task AddCategoryAsync(AddOrEditCategoryVM model)
        {
            _context.Categories.Add(new Category
            {
                Name = model.CategoryName,               
            });
            await _context.SaveChangesAsync();
        }

        public async Task EditCategoryAsync(AddOrEditCategoryVM model)
        {
            Category c = new Category {Id = model.CategoryId, Name = model.CategoryName, };
          
            
            _context.Categories.Update(c);
            await _context.SaveChangesAsync();
        }

        public SubCategoryVM GetSubCategories(int id)
        {
         
            var sub = _context.SubCategories.Where(p=>p.CategoryId == id).ToList();
         
            return new SubCategoryVM { 
                CategoryName = _context.Categories.Where(p => p.Id == id).Select(p => p.Name).FirstOrDefault(),
                CategoryId = id,
                SubCategories = sub
            };
        }

        public async Task AddSubCategoryAsync(AddOrEditSubCategoryVM model)
        {
            _context.SubCategories.Add(new SubCategory
            {
                CategoryId = model.CategoryId,
                Name = model.SubCategoryName,
                Id = model.SubCategoryId,

            });

            await _context.SaveChangesAsync();
        }

        public async Task EditSubCategoryAsync(AddOrEditSubCategoryVM model)
        {
            SubCategory subCategory = new SubCategory { Id = model.SubCategoryId, Name = model.SubCategoryName, CategoryId = model.CategoryId };
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();
        }

                
        public AddOrEditSubCategoryVM GetSubCategory(int id, int subCategoryId, string name)
        {
            return new AddOrEditSubCategoryVM { 
                SubCategoryId = subCategoryId, 
                CategoryId = id, 
                CategoryName = name, 
                SubCategoryName = _context.SubCategories.Where(x=>x.Id == subCategoryId).Select(x=>x.Name).FirstOrDefault() };
        }
    }
}
