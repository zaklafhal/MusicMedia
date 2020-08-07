using MusicMedia.Models;
using MusicMedia.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MusicMedia.Services
{
    public interface IArtistService
    {
        Task AddArtistAsync(ArtistDto artist, ApplicationUser user);
        void ValidateArtist(ArtistDto artist, ApplicationUser user);
        List<Artist> GetArtists(ApplicationUser user);
    }
}
