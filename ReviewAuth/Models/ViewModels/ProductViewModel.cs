using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Models.ViewModels
{
        public class ProductViewModel
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string UserName { get; set; }

            public int TotalReview { get; set; }
               
            public User User { get; set; }

            public double AverageRating { get; set; }
        }

        public class CreateProductViewModel
        {
            [Required(ErrorMessage = "Product name is required")]
            [Display(Name = "Product Name:")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Product Description is required")]
            [Display(Description = "Product Description:")]
            public string Description { get; set; }

        }

        public class UpdateProductViewModel
        {
            public Guid Id { get; set; }

            [Required(ErrorMessage = "Product name is required")]
            [Display(Name = "Product Name:")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Product Description is required")]
            [Display(Description = "Product Description:")]
            public string Description { get; set; }

            public User User { get; set; }
        }


    
}
