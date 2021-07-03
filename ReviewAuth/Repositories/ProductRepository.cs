
using Microsoft.EntityFrameworkCore;
using ProductReviewAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductAuthenticationDbContext _dbContext;

        public ProductRepository(ProductAuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product FindById(Guid id)
        {
            return _dbContext.Products.Find(id);
        }

        public Product UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return product;
        }

        public void Delete(Guid id)
        {
            var product = FindById(id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public List<Product> GetProducts(Guid userId)
        {
            return _dbContext.Products.Where(p => p.User.Id == userId).ToList();
        }

        public List<Product> GetReviewProducts(Guid userId)
        {
            return _dbContext.Products.Where(product => product.User.Id != userId).ToList();
        }

        public int TotalNoOfProducts (Guid userId)
        {
            return _dbContext.Products.Where(p => p.UserId == userId).Count();
        }

        

    }
}
