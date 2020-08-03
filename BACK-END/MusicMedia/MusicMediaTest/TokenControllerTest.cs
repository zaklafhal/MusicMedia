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

            var access_token = "myAccessToken";

            var token = new Token(access_token);

            tokenServiceMock.Setup(s => s.GenerateToken(loginRequest.Email)).ReturnsAsync(token);

            var controller = new TokenController(tokenServiceMock.Object);

            var result = await controller.Post(loginRequest) as ObjectResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(token, result.Value);         
        }
        [Fact]
        public async Task TestLoginWithInvalidCredentials()
        {
            var loginRequest = new LoginRequest("test@test.com", "SecretPassw0rd");

            var tokenServiceMock = new Mock<ITokenService>();

            tokenServiceMock.Setup(s => s.IsValidUser(loginRequest)).ReturnsAsync(false);

            var controller = new TokenController(tokenServiceMock.Object);

            var result = await controller.Post(loginRequest) as ObjectResult;

            var errorMsg = "Invalid Email or Password";

            Assert.Equal(400, result.StatusCode);
            Assert.Equal(errorMsg, result.Value);
        }
    }
}
