using ProductReviewAuthentication.Models;
using ProductReviewAuthentication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Services
{
    public interface IReviewService
    {
        public Review AddReview(string comment, int Ratings, Guid userId, Guid productId);

        public IEnumerable<Review> GetReviews(Guid userId);
        
        public Review FindById(Guid id);

        public List<Review> FindByProductId(Guid productId);

        public List<ProductIndexViewModel> GetReviewsPerProduct(Guid productId);

        public Review UpdateReview(Guid reviewId, string comment, int ratings);

        public void Delete(Guid id);

        public int TotalNoOfReviews(Guid userId);

        public int TotalReviewsPerProduct(Guid productId);
    }
}
