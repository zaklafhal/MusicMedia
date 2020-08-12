using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MusicMedia.Data;
using MusicMedia.Models;
using MusicMedia.Models.Dto;
using MusicMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_validate)");

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            ArtistDto model = null;

            var user = new ApplicationUser();

            Assert.Throws<Exception>(() => service.ValidateArtist(model, user));
            
        }
        [Fact]
        public void TestValidateArtistWithUserNull()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_validate)");

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            var model = new ArtistDto("1", "josh", "firstImage");

            ApplicationUser user = null;

            Assert.Throws<Exception>(() => service.ValidateArtist(model, user));

        }
        [Fact]
        public void TestValidateArtistWithUserAndModelNull()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_validate)");

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            ArtistDto model = null;

            ApplicationUser user = null;

            Assert.Throws<Exception>(() => service.ValidateArtist(model, user));

        }
        [Fact]
        public void TestValidateArtistWorkingCase()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_validate)");

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
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_add)");

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
        [Fact]
        public void TestGetArtistsSimpleCase()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_get)");

            var user =  new ApplicationUser();

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            var firstArtist = new Artist("1", "josh", "firstImage");
            var secondArtist = new Artist("2", "jack", "secondImage");
            var thirdArtist = new Artist("3", "jhon", "thirdImage");


            user.Artists.Add(firstArtist);
            user.Artists.Add(secondArtist);
            user.Artists.Add(thirdArtist);

            var result = service.GetArtists(user);

            Assert.NotEmpty(result);
            Assert.Equal(user.Artists.Count, result.Count);
            Assert.Equal(user.Artists[0].SpotifyId, result[0].SpotifyId); 
            Assert.Equal(user.Artists[1].SpotifyId, result[1].SpotifyId);
            Assert.Equal(user.Artists[2].SpotifyId, result[2].SpotifyId);
        }
        [Fact]
        public void TestGetArtistsWithNullUser()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_get)");

            ApplicationUser user = null;

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            Assert.Throws<Exception>(() => service.GetArtists(user));
        }
        [Fact]
        public void TestGetArtistsWithUserWithNoArtists()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_get)");

            var user = new ApplicationUser();

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            Assert.Throws<Exception>(() => service.GetArtists(user));
        }
        [Fact]
        public async Task TestRemoveArtistsAsyncSimpleCase()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_remove)");

            var user = new ApplicationUser();

            user.Id = "firtstId";

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            var model = new ArtistDto("1", "josh", "firstImage");

            var artist = new Artist(model);

            user.Artists.Add(artist);

            artist.ApplicationUserId = user.Id;

            artist.Id = 1;
            context.Artists.Add(artist);
            context.ApplicationUser.Add(user);
            context.SaveChanges();

            await service.RemoveArtistAsync(model, user);

            Assert.Empty(user.Artists);
            Assert.Empty(context.Artists);
        }
        [Fact]
        public async Task TestRemoveArtistsAsyncSimpleCaseWithMoreThanOneArtist()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test_REMOVE)");

            var user = new ApplicationUser();

            user.Id = "firtstId";

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var service = new ArtistService(context);

            var model = new ArtistDto("1", "josh", "firstImage");
            

            var artist = new Artist(model);
            var secondArtist = new Artist("2", "jack", "secondImage");
            user.Artists.Add(artist);
            user.Artists.Add(secondArtist);

            artist.ApplicationUserId = user.Id;
            secondArtist.ApplicationUserId = user.Id;
            artist.Id = 1;
            secondArtist.Id = 2;
            context.Artists.Add(artist);
            context.Artists.Add(secondArtist);
            context.ApplicationUser.Add(user);
            context.SaveChanges();

            await service.RemoveArtistAsync(model, user);

            Assert.NotEmpty(user.Artists);
            Assert.NotEmpty(context.Artists);
            Assert.Equal(user.Artists.Count,  context.Artists.ToList().Count);
        }
    }
}
