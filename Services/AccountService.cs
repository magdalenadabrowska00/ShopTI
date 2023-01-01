using Microsoft.AspNetCore.Identity;
using Serilog;
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

            Log.Information("Zarejestrowano użytkownika o danych: {0} {1} o adresie email {2}", newUser.FirstName, newUser.LastName, newUser.Email);
        }

        public int SignInUser(Login userSignIn)
        {
            var userFromDb = _dbContext.Users.FirstOrDefault(x => x.Email == userSignIn.Email);

            if (userFromDb == null)
            {
                Log.Error("Nie udało się zalogować użytkownikowi o adresie email {0}.", userSignIn.Email);
                throw new Exception("Taki użytkownik nie istnieje.");
            }

            var verifyResult = _passwordHasher.VerifyHashedPassword(userFromDb, userFromDb.PasswordHash, userSignIn.Password);

            if (verifyResult != PasswordVerificationResult.Success)
            {
                Log.Error("Nie udało się zalogować użytkownikowi o adresie email {0}.", userSignIn.Email);
                throw new Exception("Niepoprawne hasło");              
            }

            Log.Information("Zalogowano użytkownika o adresie email {0}.", userSignIn.Email);
            return userFromDb.UserId;
        }
    }
}
