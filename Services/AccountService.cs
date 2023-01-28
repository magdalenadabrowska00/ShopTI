using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ShopTI.Entities;
using ShopTI.IServices;
using ShopTI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopTI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ShopDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AccountService(
            ShopDbContext dbContext, 
            IPasswordHasher<User> passwordHasher,
            ITokenService tokenService)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
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

            Log.Information("Zarejestrowano użytkownika o danych;{0};{1};{2}", newUser.Email, newUser.FirstName, newUser.LastName);
        }

        public AuthenticatedResponse SignInUser(Login userSignIn)
        {
            var userFromDb = _dbContext.Users.FirstOrDefault(x => x.Email == userSignIn.Email);

            if (userFromDb == null)
            {
                Log.Error("Użytkownik o takim mailu nie istnieje;{0}", userSignIn.Email);
                throw new Exception("Taki użytkownik nie istnieje.");
            }

            var verifyResult = _passwordHasher.VerifyHashedPassword(userFromDb, userFromDb.PasswordHash, userSignIn.Password);

            if (verifyResult != PasswordVerificationResult.Success)
            {
                Log.Error("Nie udało się zalogować użytkownikowi o adresie email;{0}", userSignIn.Email);
                throw new Exception("Niepoprawne hasło");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userSignIn.Email),
                new Claim(ClaimTypes.NameIdentifier, $"{userFromDb.UserId}"),
                new Claim(ClaimTypes.Role, $"{userFromDb.Role}")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            userFromDb.RefreshToken = refreshToken;
            _dbContext.SaveChanges();

            Log.Information("Zalogowano użytkownika o danych;{0};{1};{2}", userSignIn.Email, userFromDb.Country, userFromDb.City);
            return new AuthenticatedResponse { Token = accessToken, RefreshToken = refreshToken };
        }
    }
}
