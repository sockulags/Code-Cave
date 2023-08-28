using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lucas_2._0.Models;
using lucas_2._0.Views.Admin;

namespace lucas_2._0.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly DataService _dataService;

        public AdminController(ApplicationContext context, DataService dataService)
        {
            _context = context;
            _dataService = dataService;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
              return View(await _dataService.IndexView());
        }

        // GET: Admin/SubCategory/5
        public async Task<IActionResult> SubCategory(int id)
        {
                       
            return View(_dataService.GetSubCategories(id));
        }
        //GET Admin/AddOrEditCategory
        public IActionResult AddOrEditCategory(int? id)
        {
            if (id == null)
                return View(new AddOrEditCategoryVM());
            else
                return View(_dataService.GetCategory(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditCategory(AddOrEditCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.CategoryId == 0)
                    await _dataService.AddCategoryAsync(model);
                else
                    await _dataService.EditCategoryAsync(model);

               
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //GET Admin/AddOrEditSubCategory
        public IActionResult AddOrEditSubCategory(int? id, int subCategoryId, string categoryName)
        {
            if (subCategoryId == null)
                return View(new AddOrEditSubCategoryVM() { CategoryId = subCategoryId, CategoryName = categoryName });
            else
                return View(_dataService.GetSubCategory(id, subCategoryId, categoryName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditSubCategory(AddOrEditSubCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.SubCategoryId == 0)
                    await _dataService.AddSubCategoryAsync(model);
                else
                    await _dataService.EditSubCategoryAsync(model);


                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }


        //GET Admin/Posts
        public IActionResult Posts(int subCategoryId)
        {
            return View(_dataService.GetListOfPosts(subCategoryId));
        }

        //GET Admin/AddOrEditPosts
        public IActionResult AddOrEditPosts(int? id, int subCategoryId)
        {
            if (id == null)
                return View(new AddOrEditPostsVM() { SubCategoryId = subCategoryId});
            else
                return View(_dataService.GetPosts(id));
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditPosts(AddOrEditPostsVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                    await _dataService.AddPostsAsync(model);
                else
                    await _dataService.EditPostsAsync(model);


                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
