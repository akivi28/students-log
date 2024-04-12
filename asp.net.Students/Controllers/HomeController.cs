using asp.net.Students.Models;
using asp.net.Students.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace asp.net.Students.Controllers
{
    public class HomeController : Controller
    {
        public ImageService _imageService { get; set; }
		public SiteContext _context { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(SiteContext context, ImageService imageService, IWebHostEnvironment hostEnvironment)
        {
            _webHostEnvironment = hostEnvironment;
			_context = context;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            return View();
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