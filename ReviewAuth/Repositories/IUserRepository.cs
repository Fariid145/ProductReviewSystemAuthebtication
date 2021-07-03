using ProductReviewAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Repositories
{
    public interface IUserRepository
    {
        public User AddUser(User user);


        public User UpdateUser(User user);

        public User FindUserByEmail(string email);

        public User FindUserById(Guid id);

    }
}
