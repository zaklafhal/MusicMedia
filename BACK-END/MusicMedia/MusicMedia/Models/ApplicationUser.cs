﻿using Microsoft.AspNetCore.Identity;
using MusicMedia.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public List<Artist> Artists { get; set; }
        public ApplicationUser()
        {

        }
        public ApplicationUser(RegisterRequest registerRequest)
        {
            UserName = registerRequest.Email;
            Name = registerRequest.Name;
            Email = registerRequest.Email;
        }
    }
}
