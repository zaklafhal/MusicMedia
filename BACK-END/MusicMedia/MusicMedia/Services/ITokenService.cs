using MusicMedia.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Services
{
    public interface ITokenService
    {
        Task<dynamic> GenerateToken(string email);
        Task<bool> IsValidUser(LoginRequest loginRequest);
    }
}
