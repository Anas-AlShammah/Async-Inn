using Async_Inn.Models.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Async_Inn.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState);
        public Task<UserDto> Authenticate(string username, string password);
        public Task<UserDto> GetUser(ClaimsPrincipal principal);
    }
}
