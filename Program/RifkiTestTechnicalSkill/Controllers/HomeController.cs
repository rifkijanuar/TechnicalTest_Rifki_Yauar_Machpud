using Microsoft.AspNetCore.Mvc;
using RifkiTestTechnicalSkill.Models;
using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Services.Interface;
using System.Diagnostics;

namespace RifkiTestTechnicalSkill.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public async Task<IActionResult> Index(string sterm, int genreId)
        {
            IEnumerable<Product> products = await _homeService.GetProducts(sterm, genreId);
            IEnumerable<Genre> genres = await _homeService.Genres();
            ProductDisplayModel bookModel = new ProductDisplayModel
            {
                Products = products,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId
            };
            return View(bookModel);
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
