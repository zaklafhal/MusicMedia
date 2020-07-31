using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MusicMedia.Data;
using MusicMedia.Models;
using MusicMedia.Models.Dto;
using MusicMedia.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MusicMediaTest
{
    public class ArtistServiceTest


    {
        [Fact]
        public void TestValidateArtistWithModelNull()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test)");

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            ArtistDto model = null;

            var user = new ApplicationUser();

            Assert.Throws<Exception>(() => service.ValidateArtist(model, user));
            
        }
    }
}
