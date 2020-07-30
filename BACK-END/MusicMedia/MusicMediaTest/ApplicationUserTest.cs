using Castle.Core.Internal;
using MusicMedia.Models;
using MusicMedia.Models.Dto;
using MusicMedia.Services;
using System;
using Xunit;

namespace MusicMediaTest
{
    public class ApplicationUserTest
    {
        [Fact]
        public void TestGetArtistsDtoWithNoArtists()
        {
            var user = new ApplicationUser();
            var artistsDto = user.GetArtistDtos();

            Assert.Empty(artistsDto);
        }
        [Fact]
        public void TestGetArtistsDtoWithArtists()
        {
            var user = new ApplicationUser();

            var firstArtist = new Artist("1", "josh", "firstImage");
            var secondArtist = new Artist("2", "jack", "secondImage");
            var thirdArtist = new Artist("3", "jhon", "thirdImage");

            user.Artists.Add(firstArtist);
            user.Artists.Add(secondArtist);
            user.Artists.Add(thirdArtist);

            var artistsDto = user.GetArtistDtos();


            Assert.False(artistsDto.IsNullOrEmpty());
            Assert.Equal("1", artistsDto[0].SpotifyId);
            Assert.Equal("2", artistsDto[1].SpotifyId);
            Assert.Equal("3", artistsDto[2].SpotifyId);
        }
    }
}
