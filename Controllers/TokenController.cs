using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ShopDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public TokenController(ShopDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var email = principal.Identity.Name;

            var user = _dbContext.Users.SingleOrDefault(x => x.Email == email);

            if (user is null || user.RefreshToken != refreshToken)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            return Ok(new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var email = User.Identity.Name;
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);
            if (user == null) return BadRequest();
            user.RefreshToken = null;
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
