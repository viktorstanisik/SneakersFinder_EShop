using EShop_Client_Domain.Models;
using EShop_Client_Services.Services.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_Services.Services.Users
{
    public interface IUserService
    {
        Task<int> CreateUser(UserDto userDtoModel);
        Task<JwtResponseModel> LoginUser(LoginModel userDtoModel);
    }
}
