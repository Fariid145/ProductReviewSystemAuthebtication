using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductReviewAuthentication.Models;
using ProductReviewAuthentication.Models.ViewModels;
using ProductReviewAuthentication.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;

        public HomeController(IProductService productService, IReviewService reviewService)
        {
            _productService = productService;
            _reviewService = reviewService;
        }


        public IActionResult Index( )
        {
            
            //Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult Detail(ProductViewModel model)
        {
            
            var review = _reviewService.GetReviewsPerProduct(model.Id);
            if (review == null)
            {
                return NotFound();
            }
            Product product = _productService.FindProductById(model.Id);
            ViewBag.Id = product.Name;
            return View(review);
        }

        public IActionResult IndexAuth()
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
