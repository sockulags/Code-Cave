using lucas_2._0.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lucas_2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataService _dataService;

        public HomeController(ILogger<HomeController> logger, DataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetAllPosts());
        }

        //[HttpGet("{categoryName}/{subCategoryName}")]
        //[Route("{categoryName}/{subCategoryName?}")]
        public async Task<IActionResult> SubCategoryList(string categoryName, string subCategoryName)
        {
          
                return View(await _dataService.GetSubCategoryPosts(categoryName, subCategoryName));
            
        }

        //[HttpGet("{categoryName}")]
        //[Route("{categoryName}")]
        public async Task<IActionResult> CategoryList(string categoryName)
        {
            
                return View(await _dataService.GetCategoryPosts(categoryName));
          
        }

        //[HttpGet("{categoryName}/{subCategoryName}/{postId}")]
        //[Route("{postId}")]
        public async Task<IActionResult> PostView(string categoryName, string subCategoryName,string postId)
        {
            return View(await _dataService.ShowPost(postId));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}