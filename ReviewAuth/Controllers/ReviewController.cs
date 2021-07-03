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
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ReviewController(IReviewService reviewService, IProductService productService, IUserService userService)
        {
            _reviewService = reviewService;
            _productService = productService;
            _userService = userService;
        }

       [HttpGet]
        public IActionResult Index(ProductIndexViewModel model)
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            List<ProductIndexViewModel> productIndexVM = new List<ProductIndexViewModel>();
            User user = _userService.FindUserById(userId);

            ViewBag.UserName = user.Name;
            var reviews = _reviewService.GetReviews(userId);

            foreach (var review in reviews)
            {
                ProductIndexViewModel productIndex = new ProductIndexViewModel
                {
                    Id = review.Id,
                    Comment = review.Comment,
                    Ratings = review.Ratings,
                    UserName = _userService.FindUserById(userId).Name,
                    ProductName = _productService.FindProductById(review.ProductId).Name,
                };

                productIndexVM.Add(productIndex);
            }
            return View(productIndexVM);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            User user = _userService.FindUserById(userId);

            if (user == null) return RedirectToAction(nameof(Index));


            ViewBag.products = _productService.GetProductList(userId);
            ViewBag.UserName = user.Name;
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateReviewViewModel model)
        {

            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            _reviewService.AddReview(model.Comment, model.Ratings, userId, model.ProductId);

            return RedirectToAction(nameof(Index));

        }

        [Authorize]
        public IActionResult Update(Guid id)
        {
            var review = _reviewService.FindById(id);
            if (review == null)
            {
                return NotFound();
            }
            else
            {
                Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User user = _userService.FindUserById(userId);

                ViewBag.UserName = user.Name;
                return View(review);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(UpdateReviewViewModel model)
        {

            _reviewService.UpdateReview(model.Id, model.Comment, model.Ratings);
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
            var review = _reviewService.FindById(id);
            if (review == null)
            {
                return NotFound();
            }
            _reviewService.Delete(id);
            return RedirectToAction("Index");
        }
    }

}
