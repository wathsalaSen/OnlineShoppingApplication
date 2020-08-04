using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Common.Entities;
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

        [HttpGet("users")]
        public async Task<List<User>> GetAll()
        {
            var r = await _loginBusiness.GetAll();
            return r;

        }

        //[Route("login")]
        //public async Task<IActionResult> Login(UserModel user)
        //{
        //    if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        try
        //        {
        //            await _loginBusiness.LoginUser(user).ConfigureAwait(false);
        //            return Ok();
        //        }
        //        catch (Exception)
        //        {
        //            return StatusCode(500, "Internal server error");
        //        }
        //    }
        //}

        /// <summary>
        /// This method is used to authenticate user and will return user details with JWT token. 
        /// </summary>
        /// <param name="model">This parameter will receive username of user and password of user</param>
        /// <returns></returns>

        //[Route("authenticate")]
        [AllowAnonymous]
        //[HttpPost]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    var response = _loginBusiness.Authenticate(model);
                    //if (response == null)
                    //    return BadRequest(new { message = "Username or password is incorrect" });

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            //var response = _userBusiness.Authenticate(model);

            //if (response == null)
            //    return BadRequest(new { message = "UserName or password is incorrect" });

            //return Ok(response);

        }
    }
}