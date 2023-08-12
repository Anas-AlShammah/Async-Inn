using Async_Inn.Interfaces;
using Async_Inn.Models;
using Async_Inn.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Async_Inn.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<ApplicationUser> userManager;
        private JwtTokenService jwtTokenService;

        public IdentityUserService(UserManager<ApplicationUser> manager, JwtTokenService jwtTokenService)
        {
            userManager = manager;
            this.jwtTokenService = jwtTokenService;
        }
        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (await userManager.CheckPasswordAsync(user, password))
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await jwtTokenService.GetToken(user, System.TimeSpan.FromMinutes(15))
                };
            }

            return null;

        }

        public async Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, data.Roles);
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await jwtTokenService.GetToken(user, System.TimeSpan.FromMinutes(15))

                };
            }

            // Put errors into modelState
            // Ternary:   var foo = conditionIsTrue ? goodthing : bad;
            foreach (var error in result.Errors)
            {
                var errorKey =
                  error.Code.Contains("Password") ? nameof(data.Password) :
                  error.Code.Contains("Email") ? nameof(data.Email) :
                  error.Code.Contains("UserName") ? nameof(data.Username) :
                  "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await jwtTokenService.GetToken(user, System.TimeSpan.FromMinutes(15))
            };
        }
    }
}

