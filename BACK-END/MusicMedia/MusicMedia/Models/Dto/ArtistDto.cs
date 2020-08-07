using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Models.Dto
{
    public class ArtistDto
    {
        [Required]
        public string SpotifyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        public ArtistDto()
        {

        }
        public ArtistDto(string spotifyId, string name, string image)
        {
            SpotifyId = spotifyId;
            Name = name;
            Image = image;
        }
        public virtual List<ArtistDto> GetArtistDtos(List<Artist> artists)
        {
            var artistsDtos = new List<ArtistDto>();
            foreach (var artist in artists)
            {
                var artistDto = new ArtistDto(artist);
                artistsDtos.Add(artistDto);
            }
            return artistsDtos;
        }
        public ArtistDto(Artist artist)
        {
            SpotifyId = artist.SpotifyId;
            Name = artist.Name;
            Image = artist.Image;
        }
    }
}
