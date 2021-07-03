using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ProductReviewAuthentication.Models;
using ProductReviewAuthentication.Repositories;
using System;
using System.Security.Cryptography;

namespace ProductReviewAuthentication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User FindUserById(Guid id)
        {
            return userRepository.FindUserById(id);
        }

        public User LoginUser(string email, string password)
        {
            User user = userRepository.FindUserByEmail(email);

            if (user == null)
            {
                Console.WriteLine("User not found");
                return null;
            }

            string hashedPassword = HashPassword(password, user.HashSalt);

            if (user.PasswordHash.Equals(hashedPassword))
            {
                Console.WriteLine("User is found");
                return user;
            }

            return null;
        }

        public void RegisterUser(string email, string name, string gender, string password)
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);

            string hashedPassword = HashPassword(password, saltString);

            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Name = name,
                Gender = gender,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.Now
            };

            userRepository.AddUser(user);
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            Console.WriteLine($"Hashed: {hashed}");

            return hashed;
        }
    }
}
