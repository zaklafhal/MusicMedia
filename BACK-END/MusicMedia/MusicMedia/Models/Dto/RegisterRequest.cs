using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Models.Dto
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterRequest(string email, string name ,string password, string confirmPassword)
        {
            Email = email;
            Name = name;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
