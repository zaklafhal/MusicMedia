using MusicMedia.Data;
using MusicMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMedia.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddArtistAsync(Artist artist, ApplicationUser user)
        {
            ValidateArtist(artist, user);
            user.Artists.Add(artist);
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync(); 
        }
        public void ValidateArtist(Artist artist, ApplicationUser user)
        {

            if (artist == null || user.Artists.Contains(artist))
                throw new Exception();
        }
    }
}
