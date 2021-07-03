using ProductReviewAuthentication.Models;
using ProductReviewAuthentication.Models.ViewModels;
using ProductReviewAuthentication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Services
{
    public class ReviewService : IReviewService 
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ReviewService(IReviewRepository reviewRepository, IProductService productService, IUserService userService)
        {
            _reviewRepository = reviewRepository;
            _productService = productService;
            _userService = userService;
        }


        public Review AddReview(string comment, int Ratings, Guid userId, Guid productId)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                Comment = comment,
                Ratings = Ratings,
                CreatedAt = DateTime.Now,
                User = _userService.FindUserById(userId),
                Product = _productService.FindProductById(productId)
            };
            return _reviewRepository.AddReview(review);
        }

        public IEnumerable<Review> GetReviews(Guid userId)
        {
            return _reviewRepository.GetReviews(userId);
        }

        public Review FindById(Guid id)
        {
            return _reviewRepository.FindById(id);
        }

        public List<Review> FindByProductId(Guid productId)
        {
            return _reviewRepository.FindByProductId(productId);
        }

        public List<ProductIndexViewModel> GetReviewsPerProduct(Guid productId)
        {
            var reviews = _reviewRepository.GetReviewsPerProduct(productId).Select(r => new ProductIndexViewModel
            {
                Id = r.Id,
                Comment = r.Comment,
                Ratings = r.Ratings,
                UserName = _userService.FindUserById(r.UserId).Name,
                ProductName = _productService.FindProductById(r.ProductId).Name
            }).ToList();

            return reviews;
        }

        public Review UpdateReview(Guid reviewId, string comment, int ratings)
        {
            var review = _reviewRepository.FindById(reviewId);
            review.Comment = comment;
            review.Ratings = ratings;
            return _reviewRepository.UpdateReview(review);
        }

        public void Delete(Guid id)
        {
            _reviewRepository.Delete(id);
        }

        public int TotalNoOfReviews(Guid userId)
        {
            return _reviewRepository.TotalNoOfReviews(userId);
            
        }

        public int TotalReviewsPerProduct(Guid productId)
        {

            return _reviewRepository.TotalReviewsPerProduct(productId);
        }
    }
}
