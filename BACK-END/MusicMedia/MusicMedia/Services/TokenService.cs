using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MusicMedia.Models;
using MusicMedia.Models.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicMedia.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<dynamic> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            var userInfos = new UserInfo(user);
            var claims = new List<Claim> {
                new Claim("user" , userInfos.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };
            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uhsnsdsdheweqys87272871hwn21y1")),
                        SecurityAlgorithms.HmacSha256)),
                        new JwtPayload(claims));
            var result = new { Access_Token = new JwtSecurityTokenHandler().WriteToken(token) };
            return result;
        }

        public async Task<bool> IsValidUser(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            return await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        }
    }
}
