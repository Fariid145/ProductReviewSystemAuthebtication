using ProductReviewAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Repositories
{
    public interface IReviewRepository
    {
        public Review AddReview(Review review);

        public List<Review> GetReviews(Guid userId);

        public Review FindById(Guid id);

        public List<Review> FindByProductId(Guid productId);

        public Review UpdateReview(Review review);

        public void Delete(Guid id);

        public List<Review> GetReviewsPerProduct(Guid productId);

        public double GetAverageRating(Guid productId);

        public int TotalNoOfReviews(Guid userId);

        public int TotalReviewsPerProduct(Guid productId);
    }
}
