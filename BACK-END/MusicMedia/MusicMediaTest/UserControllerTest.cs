using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MusicMedia.Controllers;
using MusicMedia.Data;
using MusicMedia.Models;
using MusicMedia.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MusicMediaTest
{
    public class UserControllerTest
    {
        [Fact]
        public async Task TestRegisterUser()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder().UseInMemoryDatabase("music_media_test)");

            var context = new ApplicationDbContext(dbOptionsBuilder.Options);

            var registerRequest = new RegisterRequest("test@test.com", "Jack", "SecretPassw0rd!", "SecretPassw0rd!");

            var user = new ApplicationUser(registerRequest);

            var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();

            var controller = new UserController(context, userManagerMock.Object);

            var result = await controller.Post(registerRequest) as OkResult;

            var users = context.ApplicationUser.ToList();
            Assert.Equal(200, result.StatusCode);
            //Assert.Equal(user.Email, users[0].Email);
            //Assert.Equal(user.Name, users[0].Name);

        }
    }
}
