using EShop_Client_Domain.Models;
using EShop_Client_Services.Services.Token;
using EShop_Client_Services.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop_Client_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("registeruser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userEntity)
        {
            try
            {
                int userId = await _userService.CreateUser(userEntity);

                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("loginuser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel loginModel)
        {
            try
            {
                var jwt = await _userService.LoginUser(loginModel);

                return Ok(jwt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
