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
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(
            ShopDbContext dbContext, 
            IPasswordHasher<User> passwordHasher, 
            AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
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

        public string SignInUser(Login userSignIn)
        {
            var userFromDb = _dbContext.Users.FirstOrDefault(x => x.Email == userSignIn.Email);

            if (userFromDb == null)
            {
                Log.Error("Użytkownik o mailu {0} nie istnieje.", userSignIn.Email);
                throw new Exception("Taki użytkownik nie istnieje.");
            }

            var verifyResult = _passwordHasher.VerifyHashedPassword(userFromDb, userFromDb.PasswordHash, userSignIn.Password);

            if (verifyResult != PasswordVerificationResult.Success)
            {
                Log.Error("Nie udało się zalogować użytkownikowi o adresie email {0}.", userSignIn.Email);
                throw new Exception("Niepoprawne hasło");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userSignIn.Email),
                new Claim(ClaimTypes.NameIdentifier, $"{userFromDb.UserId}"),
                new Claim(ClaimTypes.Role, $"{userFromDb.Role}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authenticationSettings.JwtIssuer,
                audience: _authenticationSettings.JwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays),
                signingCredentials: credentials);

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            Log.Information("Zalogowano użytkownika o adresie email {0}.", userSignIn.Email);
            return tokenAsString;
        }
    }
}
