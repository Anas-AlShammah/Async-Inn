using Async_Inn.Interfaces;
using Async_Inn.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        public UsersController(IUserService userService) 
        {
            this.userService = userService;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUser data)
        {
            var user = await userService.Register(data, this.ModelState);

            if (ModelState.IsValid)
            {
                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginData data)
        {
            var user = await userService.Authenticate(data.Username, data.Password);
            if (user != null)
            {
                return user;
            }
            return Unauthorized();
        }
    }
}
