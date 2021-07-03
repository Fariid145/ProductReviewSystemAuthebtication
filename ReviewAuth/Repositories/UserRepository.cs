using ProductReviewAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductAuthenticationDbContext _dbContext;

        public UserRepository(ProductAuthenticationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return user;
        }

        //public Role FindRole(string name)
        //{
        //    return _dbContext.Roles.FirstOrDefault(r => r.Name.Equals(name));
        //}

        public User FindUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email.Equals(email));
        }

        public User FindUserById(Guid id)
        {
            return _dbContext.Users.Find(id);
        }
    }
}
