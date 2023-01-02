using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Controllers
{
    [Route("api/account")]
    [ApiController]
    [EnableCors]
    [Authorize]
    public class AccountConstroller : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountConstroller(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult RegisterUser([FromForm] RegisterUser newUser)
        {
            _accountService.RegisterUser(newUser);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult SignInUser([FromForm] Login data)
        {
            var token = _accountService.SignInUser(data);
            return Ok(token);
        }
    }
}
