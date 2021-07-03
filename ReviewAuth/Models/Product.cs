using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Models
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        [Display(Description = "Product Description:")]
        public string Description { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
        public List<Review> Review { get; set; } = new List<Review>();
    }
}
