using Microsoft.AspNetCore.Identity;
using Moq;
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
    public class TokenServiceTest
    {
        [Fact]
        public async Task TestIsValidUserWithValidUser()
        {
            var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            var loginRequest = new LoginRequest("test@test.com", "SecretPassw0rd!");

            var user = new ApplicationUser();

            userManagerMock.Setup(u => u.FindByEmailAsync(loginRequest.Email)).ReturnsAsync(user);

            userManagerMock.Setup(u => u.CheckPasswordAsync(user, loginRequest.Password)).ReturnsAsync(true);

            var service = new TokenService(userManagerMock.Object);

            var result = await service.IsValidUser(loginRequest);

            Assert.True(result);
        }
        [Fact]
        public async Task TestIsValidUserWithInValidUser()
        {
            var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            var loginRequest = new LoginRequest("test@test.com", "SecretPassw0rd!");

            var user = new ApplicationUser();

            userManagerMock.Setup(u => u.FindByEmailAsync(loginRequest.Email)).ReturnsAsync(user);

            userManagerMock.Setup(u => u.CheckPasswordAsync(user, loginRequest.Password)).ReturnsAsync(false);

            var service = new TokenService(userManagerMock.Object);

            var result = await service.IsValidUser(loginRequest);

            Assert.False(result);
        }
        [Fact]
        public async Task TestGenerateTokenWithExistingUser()
        {
            var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            var email = "test@test.com";

            var user = new ApplicationUser();

            userManagerMock.Setup(u => u.FindByEmailAsync(email)).ReturnsAsync(user);

            var service = new TokenService(userManagerMock.Object);

            var result = await service.GenerateToken(email);

            Assert.NotNull(result);
        }
        [Fact]
        public async Task TestGenerateTokenWithInexistingUser()
        {
            var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            var email = "test@test.com";

            ApplicationUser user = null;

            userManagerMock.Setup(u => u.FindByEmailAsync(email)).ReturnsAsync(user);

            var service = new TokenService(userManagerMock.Object);

            var result = await service.GenerateToken(email);

            Assert.Null(result);
        }

    }
}
