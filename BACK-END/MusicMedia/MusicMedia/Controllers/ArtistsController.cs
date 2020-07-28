using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MusicMedia.Data;
using MusicMedia.Models;
using MusicMedia.Models.Dto;
using MusicMedia.Services;

namespace MusicMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IArtistService _artistService;
        public ArtistsController(IArtistService artistService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _artistService = artistService;
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddArtistAsync(ArtistDto model)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest();
                
                var user = await _userManager.GetUserAsync(User);
                await _artistService.AddArtistAsync(model, user);
                var artists = user.GetArtistDtos();

                return Ok(artists);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
