using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Models.ViewModels
{
    public class ReviewViewModel
        {
            public Guid Id { get; set; }

            public string Comment { get; set; }

            public int Ratings { get; set; }


            public string ProductName { get; set; }
        }

        public class ProductIndexViewModel
        {
            public Guid Id { get; set; }

            public string Comment { get; set; }

            public int Ratings { get; set; }

            public string UserName { get; set; }

            public string ProductName { get; set; }
        }

        public class CreateReviewViewModel
        {
            [Required(ErrorMessage = "You must fill out this field!")]
            [Display(Name = "Product Name:")]
            
        public Guid ProductId { get; set; }

            [Required(ErrorMessage = "Please make a Comment!!")]
            [Display(Name = "Comment Section:")]
            public string Comment { get; set; }

            [Required(ErrorMessage = "Rate Us Here!")]
            [Range(1, 5)]
            public int Ratings { get; set; }
        }
        public class UpdateReviewViewModel
        {
            public Guid Id { get; set; }

            [Required(ErrorMessage = "Please make a Comment!!")]
            [Display(Name = "Comment Section:")]
            public string Comment { get; set; }

            [Required(ErrorMessage = "Rate Us Here!")]
            [Range(1, 5)]
            public int Ratings { get; set; }
        }
    }

