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
using MusicMedia.Services;

namespace MusicMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _service;

        public TokenController(ITokenService service)
        {
            _service = service;
        }
        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="loginRequest"> An object that contains the login credentials of the user</param>
        /// <returns>An object that contains the Access_token</returns>
        // POST: api/Test
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            if (await _service.IsValidUser(loginRequest))
            {
                return Ok(await _service.GenerateToken(loginRequest.Email));
            }

            return BadRequest("Invalid Email or Password");
            
        }

    }
}
