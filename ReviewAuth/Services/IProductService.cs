

using Microsoft.AspNetCore.Mvc.Rendering;
using ProductReviewAuthentication.Models;
using ProductReviewAuthentication.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace ProductReviewAuthentication.Services
{
    public interface IProductService
    {
        public Product AddProduct(string name, string description, Guid userId);

        public List<ProductViewModel> GetProducts(Guid userId);

        public Product FindProductById(Guid id);

        public List<Product> GetProductsForReview(Guid userId);

        public IEnumerable<SelectListItem> GetProductList(Guid userId);

        public List<ProductViewModel> GetAllProducts();

        public List<ProductViewModel> GetOtherProducts(Guid userId);

        public Product UpdateProduct(Guid productId, string name, string description);

        public void Delete(Guid id);

        public int TotalNoOfProducts(Guid userId);
    }
}
