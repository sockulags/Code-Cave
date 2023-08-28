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
            return await _context.Categories.Select(p => new IndexVM
            {
                CategoryId = p.Id,
                CategoryName = p.Name,
            })
                .ToArrayAsync();
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

                
        public AddOrEditSubCategoryVM GetSubCategory(int? id, int subCategoryId, string name)
        {
            return new AddOrEditSubCategoryVM { 
                SubCategoryId = subCategoryId, 
                CategoryId = (int)id, 
                CategoryName = name, 
                SubCategoryName = _context.SubCategories.Where(x=>x.Id == subCategoryId).Select(x=>x.Name).FirstOrDefault() };
        }

        public AddOrEditPostsVM GetPosts(int? id)
        {

            var post = _context.Posts.Find(id);
            return new AddOrEditPostsVM
            {
                Id = (int)id,
                Name = post.Title,
                Description = post.Description,
                Code = post.Code,
                SubCategoryId = post.SubCategoryId,
            };
        }

        public async Task AddPostsAsync(AddOrEditPostsVM model)
        {
            _context.Posts.Add(new Post
            {
                Title = model.Name,
                Description = model.Description,
                Code = model.Code,
                SubCategoryId = model.SubCategoryId
               
            });

            await _context.SaveChangesAsync();
        }

        internal async Task EditPostsAsync(AddOrEditPostsVM model)
        {
            Post post = new Post
            {
                Id = model.Id,
                Title = model.Name,
                Description = model.Description,
                Code = model.Code,
                SubCategoryId = model.SubCategoryId,
            };
            _context.Posts.Update(post);

            await _context.SaveChangesAsync();
        }

        public PostsVM GetListOfPosts(int subCategoryId)
        {
            var list = _context.Posts.Where(p => p.SubCategoryId == subCategoryId).ToList();

            return new PostsVM
            {
                SubCategoryName = _context.SubCategories.Where(p => p.Id == subCategoryId).Select(p => p.Name).FirstOrDefault(),
                SubCategoryId = subCategoryId,
                Posts = list
            };
        }
    }
}
