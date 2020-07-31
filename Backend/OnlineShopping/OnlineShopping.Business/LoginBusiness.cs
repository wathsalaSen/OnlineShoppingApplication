using Microsoft.Extensions.Options;
using OnlineShopping.Common.Interfaces;
using OnlineShopping.Common.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace OnlineShopping.Business
{
    public class LoginBusiness : ILoginBusiness
    {

        private List<UserModel> _users = new List<UserModel>
        {  
            new UserModel { Id = 1, FirstName = "user", LastName = "name", UserName = "test", Password = "test" }
        };

        public Task<string> LoginUser(UserModel userModel)
        {
            var Result = "success";
            return Task.FromResult(Result);
        }

        private readonly AppSettingsModel _appSettings;

        public LoginBusiness(IOptions<AppSettingsModel> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// This method will filter user details from db  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AuthenticateResponseModel Authenticate(AuthenticateRequestModel model)
        {
            var user = _users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponseModel(user, token);
        }

        // helper methods

        private string generateJwtToken(UserModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
