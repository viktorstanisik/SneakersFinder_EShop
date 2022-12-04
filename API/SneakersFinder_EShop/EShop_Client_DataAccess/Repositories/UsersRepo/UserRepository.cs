using EShop_Client_Domain;
using EShop_Client_Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop_Client_DataAccess.Repositories.UsersRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly EShop_ClientDbContext _eShopClientDbContext;

        public UserRepository(EShop_ClientDbContext eShopClientDbContext)
        {
            _eShopClientDbContext = eShopClientDbContext;
        }

        public async Task<User> CreateUser(User userModel)
        {
            var userExist = await _eShopClientDbContext.User.FirstOrDefaultAsync(x => x.Email == userModel.Email);
            if (userExist != null) throw new Exception();

            try
            {
                var user = new User()
                {
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Password = userModel.Password,
                };

                await _eShopClientDbContext.AddAsync(user);
                await _eShopClientDbContext.SaveChangesAsync();

                return user;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _eShopClientDbContext.User.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _eShopClientDbContext.User.FirstOrDefaultAsync(x => x.Email == email);
        }

    }
}
