using Microsoft.AspNetCore.Mvc.Rendering;
using ProductReviewAuthentication.Models;
using ProductReviewAuthentication.Models.ViewModels;
using ProductReviewAuthentication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;
        private readonly IReviewRepository _reviewRepository;

        public ProductService(IProductRepository productRepository, IUserService userService, IReviewRepository reviewRepository)
        {
            _productRepository = productRepository;
            _userService = userService;
            _reviewRepository = reviewRepository;
        }

        public Product AddProduct(string name, string description, Guid userId)
        {
            Product product = new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                CreatedAt = DateTime.Now,
                User = _userService.FindUserById(userId)
            };

            _productRepository.AddProduct(product);

            return product;
        }
        public List<ProductViewModel> GetProducts(Guid userId)
        {
            var products = _productRepository.GetProducts(userId).Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                AverageRating = _reviewRepository.GetAverageRating(p.Id)
            }).ToList();

            return products;
        }

        public List<ProductViewModel> GetOtherProducts(Guid userId)
        {
            var products = _productRepository.GetReviewProducts(userId).Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                UserName = _userService.FindUserById(p.UserId).Name,
                TotalReview = _reviewRepository.TotalReviewsPerProduct(p.Id),
                AverageRating = _reviewRepository.GetAverageRating(p.Id)
            }).ToList();

            return products;
        }

        public List<ProductViewModel> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                UserName = _userService.FindUserById(p.UserId).Name,
                TotalReview = _reviewRepository.TotalReviewsPerProduct(p.Id),
                AverageRating = _reviewRepository.GetAverageRating(p.Id)
            }).ToList();

            return products;
        }

        public Product FindProductById(Guid id)
        {
            return _productRepository.FindById(id);
        }

        public List<Product> GetProductsForReview(Guid userId)
        {
            var products = _productRepository.GetReviewProducts(userId);
            return products;
        }

        public IEnumerable<SelectListItem> GetProductList(Guid userId)
        {
            return GetProductsForReview(userId).Select(c => new SelectListItem(c.Name, c.Id.ToString()));
        }

        public Product UpdateProduct(Guid productId, string name, string description)
        {
            var product = _productRepository.FindById(productId);
            product.Name = name;
            product.Description = description;

            return _productRepository.UpdateProduct(product);
        }

        public void Delete(Guid id)
        {
            _productRepository.Delete(id);
        }

        public int TotalNoOfProducts(Guid userId)
        {
            return _productRepository.TotalNoOfProducts(userId);
        }

    }
}
