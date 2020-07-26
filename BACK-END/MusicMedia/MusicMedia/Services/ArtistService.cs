﻿using MusicMedia.Data;
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
            var artist = new Artist(model);
            user.Artists.Add(artist);
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync(); 
        }
        public void ValidateArtist(ArtistDto model, ApplicationUser user)
        {

            if (model == null || user == null)
                throw new Exception();
            if (user.ContainsArtist(model))
                throw new Exception();
        }
    }
}
