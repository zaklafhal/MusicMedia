using Microsoft.AspNetCore.Identity;
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
        public virtual List<Artist> Artists { get; set; }
        public ApplicationUser()
        {
            Artists = new List<Artist>();
        }
        public ApplicationUser(RegisterRequest registerRequest)
        {
            UserName = registerRequest.Email;
            Name = registerRequest.Name;
            Email = registerRequest.Email;
            Artists = new List<Artist>();
        }
        public virtual List<ArtistDto> GetArtistDtos()
        {
            var artistsDtos = new List<ArtistDto>();
            foreach (var artist in Artists)
            {
                var artistDto = new ArtistDto(artist);
                artistsDtos.Add(artistDto);
            }
            return artistsDtos;
        }

        public virtual bool ContainsArtist(ArtistDto model)
        {
            var artists = Artists.Where(a => a.SpotifyId == model.SpotifyId).ToList();
            return artists.Count  != 0 ;
        }
    }
}
