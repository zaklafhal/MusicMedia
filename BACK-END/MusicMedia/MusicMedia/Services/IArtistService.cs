using MusicMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MusicMedia.Services
{
    public interface IArtistService
    {
        void AddArtist(Artist artist, ApplicationUser user);


    }
}
