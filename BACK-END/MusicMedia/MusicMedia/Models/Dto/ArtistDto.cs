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
        public ArtistDto(Artist artist)
        {
            SpotifyId = artist.SpotifyId;
            Name = artist.Name;
            Image = artist.Image;
        }
    }
}
