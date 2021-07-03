using Microsoft.EntityFrameworkCore;
using ProductReviewAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ProductAuthenticationDbContext _dbContext;

        public ReviewRepository(ProductAuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Review AddReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();
            return review;
        }

        public List<Review> GetReviews(Guid userId)
        {
            return _dbContext.Reviews.Include(r => r.User).Where(review=> review.User.Id == userId).ToList();
        }

        public Review FindById(Guid id)
        {
            return _dbContext.Reviews.Find(id);
        }

        public List<Review> FindByProductId(Guid productId)
        {
            return _dbContext.Reviews.Where(review => review.ProductId == productId).ToList();
        }

        public Review UpdateReview(Review review)
        {
            _dbContext.Reviews.Update(review);
            _dbContext.SaveChanges();
            return review;
        }

        public void Delete(Guid id)
        {
            var review = FindById(id);

            if (review != null)
            {
                _dbContext.Reviews.Remove(review);
                _dbContext.SaveChanges();
            }
        }

        public List<Review> GetReviewsPerProduct(Guid productId)
        {
            return _dbContext.Reviews.Where(review => review.ProductId == productId).ToList();
        }

        public double GetAverageRating(Guid productId)
        {
            var reviews = FindByProductId(productId);
            double sum = 0;
            if (reviews.Count == 0) return 0;
            foreach (var review in reviews)
            {
                sum += review.Ratings;
            }
            double totalaverage = sum / reviews.Count;
            return totalaverage;
        }
        public int TotalNoOfReviews(Guid userId)
        {

            return _dbContext.Reviews.Where(r => r.UserId == userId).Count();
        }

        public int TotalReviewsPerProduct(Guid productId)
        {

            return _dbContext.Reviews.Where(r => r.ProductId == productId).Count();
        }
    }
}
