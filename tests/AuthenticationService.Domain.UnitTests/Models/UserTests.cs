using AuthenticationService.Domain.Models;
using FluentAssertions;
using Xunit;

namespace AuthenticationService.Domain
{
    public class UserTests
    {
        [Fact]
        public void User_WithAllValidProperties_ShouldBeValidObject()
        {
            // Arrange
            var user = new User();

            // Action
            user.EnabledUser();
            user.SetUserName(username: "jose.c");
            user.SetHashedPassword(hashedPassword: "12345678998745632114");

            // Assert
            user.Enabled.Should().BeTrue();
            user.Username.Should().Be("jose.c");
            user.HashedPassword.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void User_WithInvalidProperties_ShouldBeInvalidObject()
        {
            // Arrange
            var user = new User();

            // Action
            user.EnabledUser();
            user.SetUserName(username: "jose.c");
            user.SetHashedPassword(hashedPassword: null);

            // Assert
            user.Enabled.Should().BeTrue();
            user.Username.Should().Be("jose.c");
            user.HashedPassword.Should().BeNull();
        }
    }
}
