using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
