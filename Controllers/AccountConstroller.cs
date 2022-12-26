using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Controllers
{
    [Route("api/account")]
    [ApiController]
    [EnableCors]
    public class AccountConstroller : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountConstroller(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUser newUser)
        {
            _accountService.RegisterUser(newUser);
            return Ok();
        }
    }
}
