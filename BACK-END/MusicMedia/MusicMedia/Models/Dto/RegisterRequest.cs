using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Models.Dto
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public RegisterRequest()
        {

        }
        public RegisterRequest(string email, string name ,string password, string confirmPassword)
        {
            Email = email;
            Name = name;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
