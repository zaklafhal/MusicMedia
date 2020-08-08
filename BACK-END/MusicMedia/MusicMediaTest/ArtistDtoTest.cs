using MusicMedia.Models;
using MusicMedia.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicMediaTest
{
    public class ArtistDtoTest
    {
        [Fact]
        public void TestGetArtistsDtosSimpleCase()
        {
            var artistDto = new ArtistDto();
            var artists = new List<Artist>();

            var firstArtist = new Artist("1", "josh", "firstImage");
            var secondArtist = new Artist("2", "jack", "secondImage");
            var thirdArtist = new Artist("3", "jhon", "thirdImage");

            artists.Add(firstArtist);
            artists.Add(secondArtist);
            artists.Add(thirdArtist);

            var artistsDto = artistDto.GetArtistDtos(artists);

            Assert.NotEmpty(artistsDto);
            Assert.Equal(firstArtist.SpotifyId, artistsDto[0].SpotifyId);
            Assert.Equal(secondArtist.SpotifyId, artistsDto[1].SpotifyId);
            Assert.Equal(thirdArtist.SpotifyId, artistsDto[2].SpotifyId);

        }
        [Fact]
        public void TestGetArtistsDtosWithEmptyArtistList()
        {
            var artistDto = new ArtistDto();
            var artists = new List<Artist>();

            var artistsDto = artistDto.GetArtistDtos(artists);

            Assert.Empty(artistsDto);  
        }
        [Fact]
        public void TestGetArtistsDtosWithNullList()
        {
            var artistDto = new ArtistDto();
            List<Artist> artists = null;

            var artistsDto = artistDto.GetArtistDtos(artists);

            Assert.Empty(artistsDto);
        }
    }
}
