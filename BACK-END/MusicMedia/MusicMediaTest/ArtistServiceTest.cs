﻿using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
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
using System.Threading.Tasks;
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
        [Fact]
        public void TestValidateArtistWithUserNull()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test)");

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            var model = new ArtistDto("1", "josh", "firstImage");

            ApplicationUser user = null;

            Assert.Throws<Exception>(() => service.ValidateArtist(model, user));

        }
        [Fact]
        public void TestValidateArtistWithUserAndModelNull()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test)");

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            ArtistDto model = null;

            ApplicationUser user = null;

            Assert.Throws<Exception>(() => service.ValidateArtist(model, user));

        }
        [Fact]
        public void TestValidateArtistWithUserContainsArtists()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test)");

            var user = new Mock<ApplicationUser>();

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            var model = new ArtistDto("1", "josh", "firstImage");

            user.Setup( a => a.ContainsArtist(model)).Returns(true);

            Assert.Throws<Exception>(() => service.ValidateArtist(model, user.Object ));
        }
        [Fact]
        public void TestValidateArtistWorkingCase()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test)");

            var user = new Mock<ApplicationUser>();

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            var model = new ArtistDto("1", "josh", "firstImage");

            user.Setup(a => a.ContainsArtist(model)).Returns(false);

            service.ValidateArtist(model, user.Object);
        }
        [Fact]
        public async Task TestAddArtistNormalCase()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test)");

            var user = new ApplicationUser();

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            var model = new ArtistDto("1", "josh", "firstImage");

            await service.AddArtistAsync(model, user);

            Assert.NotEmpty(user.Artists);
            Assert.Equal("1", user.Artists[0].SpotifyId);
            Assert.NotEmpty(context.Artists);
            Assert.Equal(user.Artists[0], await context.Artists.FirstOrDefaultAsync());
        }
    }
}