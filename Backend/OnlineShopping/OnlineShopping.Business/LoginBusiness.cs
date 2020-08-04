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
using OnlineShopping.Common.Entities;
using OnlineShopping.Data;

namespace OnlineShopping.Business
{
    public class LoginBusiness : ILoginBusiness
    {

        //private List<UserModel> _users = new List<UserModel>
        //{
        //    new UserModel { Id = 1, FirstName = "user", LastName = "name", UserName = "test", Password = "test" }
        //};

        //public Task<string> LoginUser(UserModel userModel)
        //{
        //    var Result = "success";
        //    return Task.FromResult(Result);
        //}

        private readonly AppSettingsModel _appSettings;
        private readonly IAsyncRepository<User> _iAsyncRepository;

        public LoginBusiness(IOptions<AppSettingsModel> appSettings, IAsyncRepository<User> iAsyncRepository)
        {
            _appSettings = appSettings.Value;
            this._iAsyncRepository = iAsyncRepository;
        }

        public async Task<List<User>> GetAll()
        {
            var persons = await _iAsyncRepository.FindAsync<User>
                           (x => x.MobileNumber.Equals(123));
            //return persons.Select(person => new User()).ToList();
            return persons.Select(p => new User
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address = p.Address,
                MobileNumber = p.MobileNumber,
                Email = p.Email,
                Password = p.Password,
                UserName = p.UserName
            }).ToList();         
        }
        /// <summary>
        /// This method will filter user details from db  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AuthenticateResponseModel> Authenticate(AuthenticateRequestModel model)
        {
            try
            {
                var user = await this._iAsyncRepository.SingleOrDefaultAsync<User>(x => x.UserName == model.Username && x.Password == model.Password).ConfigureAwait(false);
                //var user = result.Select(person => new User()).Take(1);
                //var user = _users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

                // return null if user not found
                if (user == null) return null;

                // authentication successful so generate jwt token
                var token = generateJwtToken(user);

                return new AuthenticateResponseModel(user, token);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // helper methods

        private string generateJwtToken(User user)
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
