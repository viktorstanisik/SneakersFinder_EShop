using EShop_Client_DataAccess.Repositories.UsersRepo;
using EShop_Client_Domain.Models;
using EShop_Client_Services.Services.Token;
using EShop_Client_Shared.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EShop_Client_Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, ITokenService tokenService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<int> CreateUser(UserDto userDtoModel)
        {
            await RequiredField(userDtoModel);

            userDtoModel.Password = Methods.Sha512Hash(userDtoModel.Password);

            var user = await _userRepository.CreateUser(userDtoModel.ToDomain());

            return user.Id;
        }

        public async Task<JwtResponseModel> LoginUser(LoginModel model)
        {
            User user = await _userRepository.GetUser(model.Email, Methods.Sha512Hash(model.Password));
            if (user is null) throw new Exception();

            var token = _tokenService.GenerateJwtToken(model, user.Id);
            JwtResponseModel jwtToken = new() { Jwt = token };
            return jwtToken;
        }

        public async Task<bool> RequiredField(UserDto model)
        {

            Regex emailRegex = new Regex(_configuration["RegexValidation:EmailRegex"]);
            Regex passwordRegex = new Regex(_configuration["RegexValidation:PasswordRegex"]);

            User currentUser = await _userRepository.GetUserByEmail(model.Email);

            if (currentUser != null) throw new Exception();

            if (!emailRegex.IsMatch(model.Email.Trim()))
                throw new Exception();

            if (!passwordRegex.IsMatch(model.Password.Trim()))
                throw new Exception();

            return await Task.FromResult(true);
        }
    }
}
