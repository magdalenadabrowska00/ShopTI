﻿using Microsoft.AspNetCore.Cors;
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
        public ActionResult RegisterUser([FromForm] RegisterUser newUser)
        {
            _accountService.RegisterUser(newUser);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult SignInUser([FromForm] Login data)
        {
            _accountService.SignInUser(data);
            return Ok();
        }
    }
}
