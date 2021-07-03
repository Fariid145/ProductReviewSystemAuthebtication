using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string PasswordHash { get; set; }

        public string HashSalt { get; set; }

        public List<Product> Product { get; set; } = new List<Product>();

        public List<Review> Review { get; set; } = new List<Review>();
    }
}
