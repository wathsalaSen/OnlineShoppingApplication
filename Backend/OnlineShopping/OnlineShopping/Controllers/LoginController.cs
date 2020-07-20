using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Common.Interfaces;
using OnlineShopping.Common.Models;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;

        public LoginController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [Route("login")]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    await _loginBusiness.LoginUser(user).ConfigureAwait(false);
                    return Ok();
                }
                catch (Exception)
                {
                    return StatusCode(500, "Internal server error");
                }
            }
        }
}