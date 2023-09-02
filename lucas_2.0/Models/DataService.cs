using AspNetCore;
using lucas_2._0.Views.Admin;
using lucas_2._0.Views.Home;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace lucas_2._0.Models
{
    public class DataService
    {
        public readonly ApplicationContext _context;
        public DataService(ApplicationContext context) {
            _context = context;
        }

        public async Task<Views.Admin.IndexVM[]> IndexView()
        {
            return await _context.Categories.Select(p => new Views.Admin.IndexVM
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

        public async Task<Views.Home.IndexVM[]> GetAllPosts()
        {
            var model = await _context.Posts
                .OrderByDescending(p=>p.CreatedDate)
                .Select(p => new Views.Home.IndexVM()
            {
                PostName = p.Title,
                PostDescription = p.Description,
                SubCategoryName = p.SubCategory.Name,
                CategoryName = _context.SubCategories.Where(o=>o.Name == p.SubCategory.Name).Select(p=>p.Category.Name).FirstOrDefault(),
            }).Take(10).ToArrayAsync();
            return  model;
        }

        public async Task<CategoryListVM[]> GetCategoryPosts(string categoryName)
        {
            var id = await _context.Categories
                .Where(p => p.Name == categoryName)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();

            var categoryPosts = await _context.Posts
                .Where(p => _context.SubCategories
                    .Where(sub => sub.CategoryId == id)
                    .Any(sub => sub.Id == p.SubCategoryId))
                .Include(p => p.SubCategory)
                .Select(p => new CategoryListVM()
                {
                    PostName = p.Title,
                    PostDescription = p.Description,
                    SubCategoryName = p.SubCategory.Name,
                    CategoryName = categoryName
                })
                .ToArrayAsync();

            return categoryPosts;
        }


        public async Task<CategoryListVM[]> GetSubCategoryPosts(string categoryName, string subCategoryName)
        {
            var id = await _context.Categories
                .Where(p => p.Name == categoryName)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();

            var subCategoryId = await _context.SubCategories
                .Where(sub => sub.Name == subCategoryName)
                .Select(sub => sub.Id)
                .FirstOrDefaultAsync();

            var categoryPosts = await _context.Posts
                .Where(p => p.SubCategoryId == subCategoryId)
                .Include(p => p.SubCategory)
                .Select(p => new CategoryListVM()
                {
                    PostName = p.Title,
                    PostDescription = p.Description,
                    SubCategoryName = p.SubCategory.Name,
                    CategoryName = categoryName
                })
                .ToArrayAsync();

            return categoryPosts;
        }

        public async Task<PostViewVM> ShowPost(string postId)
        {
            var post = _context.Posts.Where(p => p.Title == postId).FirstOrDefault();

            return new PostViewVM
            {
                Title = post.Title,
                Description = post.Description,
                Code = post.Code,
                Created = post.CreatedDate
            };
        }
    }
}
