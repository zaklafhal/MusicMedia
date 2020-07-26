using MusicMedia.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string SpotifyId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ApplicationUserId { get; set; }

        public Artist()
        {

        }
        public Artist(ArtistDto model)
        {
            SpotifyId = model.SpotifyId;
            Name = model.Name;
            Image = model.Image;
        }
    }
}
