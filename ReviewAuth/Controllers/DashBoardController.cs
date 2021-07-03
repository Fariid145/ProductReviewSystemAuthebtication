using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductReviewAuthentication.Models;
using ProductReviewAuthentication.Models.ViewModels;
using ProductReviewAuthentication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;

        public DashBoardController(IProductService productService, IReviewService reviewService, IUserService userService)
        {
            _productService = productService;
            _reviewService = reviewService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var products = _productService.GetOtherProducts(userId);
            User user = _userService.FindUserById(userId);

            ViewBag.UserName = user.Name;
            ViewBag.TotalProduct = _productService.TotalNoOfProducts(userId);
            ViewBag.TotalReviews = _reviewService.TotalNoOfReviews(userId);
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
    }
}
