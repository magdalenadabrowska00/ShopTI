using Microsoft.AspNetCore.Identity;
using ShopTI.Entities;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ShopDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(ShopDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void RegisterUser(RegisterUser newUser)
        {
            var user = new User()
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Role = newUser.Role,
                Country = newUser.Country,
                City = newUser.City,
                Street = newUser.Street,
                PostalCode = newUser.PostalCode
            };

            var hashedPassword = _passwordHasher.HashPassword(user, newUser.Password);

            user.PasswordHash = hashedPassword;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
