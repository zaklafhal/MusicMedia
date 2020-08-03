using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicMedia.Controllers;
using MusicMedia.Models.Dto;
using MusicMedia.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicMediaTest
{
    public class TokenControllerTest
    {
        [Fact]
        public async Task TestLoginWithValidCredentials()
        {
            var loginRequest = new LoginRequest("test@test.com", "SecretPassw0rd");

            var tokenServiceMock = new Mock<ITokenService>();

            tokenServiceMock.Setup(s => s.IsValidUser(loginRequest)).ReturnsAsync(true);

            dynamic token = new { Access_token = "myAccessToken" };

            tokenServiceMock.Setup(s => s.GenerateToken(loginRequest.Email)).Returns(token);

            var controller = new TokenController(tokenServiceMock.Object);

            var result = await controller.Post(loginRequest) as ObjectResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(token, result.Value);         
        }

    }
}
