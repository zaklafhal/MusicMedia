using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicMedia.Data;
using MusicMedia.Models;
using MusicMedia.Models.Dto;

namespace MusicMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // POST: api/Test
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            if (await IsValidUser(loginRequest))
            {
                return new ObjectResult(await GenerateToken(loginRequest.Email));
            }
            else
            {
                return BadRequest();
            }

        }

        private async Task<dynamic> GenerateToken(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
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
        private async Task<bool> IsValidUser(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            return await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        }
    }
}
