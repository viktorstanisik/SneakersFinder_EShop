using EShop_Client_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_DataAccess.Repositories.UsersRepo
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetUser(string email, string password);
        Task<User> GetUserByEmail(string email);

    }
}
