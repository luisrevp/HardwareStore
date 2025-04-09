using System.ComponentModel.DataAnnotations;

namespace HardwareStore.BE.Models.User
{
    public class UserAddDto
    {
        [MinLength(2)]
        [MaxLength(100)]
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [MinLength(2)]
        [MaxLength(250)]
        [Required(ErrorMessage = "You must provide an email!")]
        public string Email { get; set; }
        //[MinLength(8)]
        //[Required(ErrorMessage = "You must provide your email confirmation!")]
        //public string EmailConfirmed { get; set; }
        [MinLength(8)]
        [Required(ErrorMessage = "You must provide a password!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "You must provide your Date of Birth!")]
        public DateTime DoB { get; set; }
        [Required(ErrorMessage = "You must provide an username")]
        public string UserName { get; set; }
    }
}
