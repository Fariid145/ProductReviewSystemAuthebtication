using ProductReviewAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Repositories
{
    public interface IProductRepository
    {
        public Product AddProduct(Product product);

        public Product FindById(Guid id);

        public Product UpdateProduct(Product product);

        public List<Product> GetAllProducts();

        public void Delete(Guid id);

        public List<Product> GetProducts(Guid userId);

        public List<Product> GetReviewProducts(Guid userId);

        public int TotalNoOfProducts(Guid userId);


    }
}
