using System.ComponentModel.DataAnnotations;

namespace Async_Inn.Models.Dtos
{
    public class LoginData
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
