using Async_Inn.Models.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState);
        public Task<UserDto> Authenticate(string username, string password);
    }
}
