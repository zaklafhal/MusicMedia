using Microsoft.EntityFrameworkCore;
using MusicMedia.Data;
using MusicMedia.Models;
using MusicMedia.Models.Dto;
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
        public async Task AddArtistAsync(ArtistDto model, ApplicationUser user)
        {
            ValidateArtist(model, user);
            if (user.ContainsArtist(model))
                throw new Exception("The artists is already in the user list");
            var artist = new Artist(model);
            artist.ApplicationUserId = user.Id;
            user.Artists.Add(artist);
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync(); 
        }

        public List<Artist> GetArtists(ApplicationUser user)
        {
            if(user == null)
                throw new Exception();

            if (!user.HasArtists())
                throw new Exception("The user does not have artists on his list !");

            return user.Artists;
        }

        public async Task RemoveArtistAsync(ArtistDto model, ApplicationUser user)
        {
            ValidateArtist(model, user);
            if (!user.ContainsArtist(model))
                throw new Exception("The artists is not in the user list");
            var artist = await _context.Artists.Where(a => a.ApplicationUserId == user.Id && a.SpotifyId == model.SpotifyId).FirstOrDefaultAsync();
            user.Artists.Remove(artist);
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
        }

        public void ValidateArtist(ArtistDto model, ApplicationUser user)
        {

            if (model == null || user == null)
                throw new Exception();
        }
    }
}
