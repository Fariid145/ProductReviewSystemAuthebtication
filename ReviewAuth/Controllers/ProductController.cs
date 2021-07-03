using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;


        public ProductController(IProductService productService, IUserService userService, IReviewService reviewService)
        {
            _productService = productService;
            _userService = userService;
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var products = _productService.GetProducts(userId);
            User user = _userService.FindUserById(userId);

            ViewBag.UserName = user.Name;
            return View(products);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            ViewBag.UserName = user.Name;
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateProductViewModel model)
        {

            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            Product p = _productService.AddProduct(model.Name, model.Description, userId);
            
            return RedirectToAction("Index");

        }

        [Authorize]
        public IActionResult Update(Guid id)
        {
            var product = _productService.FindProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User user = _userService.FindUserById(userId);

                ViewBag.UserName = user.Name;
                return View(product);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(UpdateProductViewModel model)
        {
            
            _productService.UpdateProduct(model.Id, model.Name, model.Description);
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet]
        public IActionResult DeleteOption()
        {
            return View();
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var product = _productService.FindProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.Delete(id);
            return RedirectToAction("Index");
        }

       
        public IActionResult ReviewsPerProduct(ProductViewModel model)
        {
            Console.WriteLine(model.Id);
            var review = _reviewService.GetReviewsPerProduct(model.Id);
            if (review == null)
            {
                return NotFound();
            }
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User user = _userService.FindUserById(userId);

            ViewBag.UserName = user.Name;
            Product product = _productService.FindProductById(model.Id);
            ViewBag.Id = product.Name;
            return View(review);
        }
    }
}
