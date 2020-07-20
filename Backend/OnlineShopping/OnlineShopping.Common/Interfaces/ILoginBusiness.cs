using OnlineShopping.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Common.Interfaces
{
    public interface ILoginBusiness
    {
        Task<string> LoginUser(UserModel userModel);
    }
}
