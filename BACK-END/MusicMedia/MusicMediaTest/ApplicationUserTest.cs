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
    }
}
