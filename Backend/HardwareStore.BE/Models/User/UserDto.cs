using System.ComponentModel.DataAnnotations;

namespace HardwareStore.BE.Models.User
{
    public class UserDto
    {
        [Required(ErrorMessage = "You must provide a password!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "You must provide an username!")]
        public string UserName { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
