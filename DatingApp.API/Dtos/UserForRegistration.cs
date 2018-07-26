using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegistration
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Password required length between 6 and 12")]
        public string Password { get; set; }
    }
}