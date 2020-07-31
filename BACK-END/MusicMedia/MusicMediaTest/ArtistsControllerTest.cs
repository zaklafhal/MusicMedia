using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MusicMedia.Controllers;
using MusicMedia.Data;
using MusicMedia.Models;
using MusicMedia.Models.Dto;
using MusicMedia.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicMediaTest
{
    public class ArtistsControllerTest
    {
        [Fact]
        public async Task TestAddControllerSimpleCase()
        {

            var model = new ArtistDto("1", "josh", "firstImage");

            var userMock = new Mock<ApplicationUser>();

            userMock.Setup(u => u.GetArtistDtos()).Returns(new List<ArtistDto>() { model });

            var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(userMock.Object);

            var artistServiceMock = new Mock<IArtistService>();

            artistServiceMock.Setup(s => s.AddArtistAsync(model, userMock.Object)).Verifiable();

            var controller = new ArtistsController(artistServiceMock.Object, userManagerMock.Object);

            var result = await controller.AddArtistAsync(model) as ObjectResult;

            var status = result.StatusCode;

            var artistsDto = result.Value as List<ArtistDto>;

            Assert.Equal(200, status);
            Assert.Equal(model.SpotifyId, artistsDto[0].SpotifyId);
            Assert.Equal(model.Name, artistsDto[0].Name);
            Assert.Equal(model.Image, artistsDto[0].Image);
        }
        [Fact]
        public async Task TestAddControllerWithBadRequest()
        {

            var model = new ArtistDto("1", "josh", "firstImage");

            var userMock = new Mock<ApplicationUser>();

            userMock.Setup(u => u.GetArtistDtos()).Returns(new List<ArtistDto>() { model });

            var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(userMock.Object);

            var artistServiceMock = new Mock<IArtistService>();

            artistServiceMock.Setup(s => s.AddArtistAsync(model, userMock.Object)).Throws(new Exception());

            var controller = new ArtistsController(artistServiceMock.Object, userManagerMock.Object);

            var result = await controller.AddArtistAsync(model) as ObjectResult;

            var status = result.StatusCode;

            Assert.Equal(400, status);
        }
    }
}
