using AuthenticationService.Application.Request;
using AuthenticationService.Domain.Models;
using AuthenticationService.Domain.Repository;
using AuthenticationService.Domain.Services;
using AuthenticationService.Shared.ViewModels;
using FluentAssertions;
using Moq;
using OperationResult;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AuthenticationService.Application.Handlers
{
    public sealed class AuthenticateUserRequestHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ITokenGeneratorService> _tokenGeneratorService;
        private readonly AuthenticateUserRequestHandler _sut;
        public AuthenticateUserRequestHandlerTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _tokenGeneratorService = new Mock<ITokenGeneratorService>();
            _sut = new AuthenticateUserRequestHandler(_userRepository.Object, _tokenGeneratorService.Object);
        }

        [Fact]
        public async Task AuthenticateUser_WithAllValidProperties_ShouldBeSuccess()
        {
            // Arrange
            var validAndEnabledUser = new User();
            validAndEnabledUser.EnabledUser();
            validAndEnabledUser.SetHashedPassword("123456");
            validAndEnabledUser.SetUserName("jose.c");

            var request = new AuthenticateUserRequest 
            {
                HashedPassword = "123456",
                Username = "jose.c"
            };

            var response = new AuthenticatedUserViewModel 
            { 
                Token = "123456",
                Username = "jose.c"
            };

            // Action
            var sut = new AuthenticateUserRequestHandler(_userRepository.Object, _tokenGeneratorService.Object);

            _userRepository.Setup(data => data.AuthenticateUser("jose.c"))
                .Returns(Result.Success(validAndEnabledUser));

            _tokenGeneratorService.Setup(token => token.GenerateToken(validAndEnabledUser))
                .Returns(Result.Success(response));

            var result = await sut.Handle(request, new CancellationToken());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Token.Should().Be("123456");
        }

        [Fact]
        public async Task AuthenticateUser_WithInvalidPassword_ShouldBeError()
        {
            // Arrange
            var validAndEnabledUser = new User();
            validAndEnabledUser.EnabledUser();
            validAndEnabledUser.SetHashedPassword("1234567");
            validAndEnabledUser.SetUserName("jose.c");

            var request = new AuthenticateUserRequest
            {
                HashedPassword = "123456",
                Username = "jose.c"
            };

            // Action
            var sut = new AuthenticateUserRequestHandler(_userRepository.Object, _tokenGeneratorService.Object);

            _userRepository.Setup(data => data.AuthenticateUser("jose.c"))
                .Returns(Result.Success(validAndEnabledUser));

            var result = await sut.Handle(request, new CancellationToken());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task AuthenticateUser_WithInvalidUser_ShouldBeError()
        {
            // Arrange
            var invalidUser = new User();
            invalidUser.EnabledUser();
            invalidUser.SetHashedPassword("1234567");
            invalidUser.SetUserName("jose.c");

            invalidUser = null;

            var request = new AuthenticateUserRequest
            {
                HashedPassword = "123456",
                Username = "jose.c"
            };

            // Action
            var sut = new AuthenticateUserRequestHandler(_userRepository.Object, _tokenGeneratorService.Object);

            _userRepository.Setup(data => data.AuthenticateUser("jose.c"))
                .Returns(Result.Success(invalidUser));            

            var result = await sut.Handle(request, new CancellationToken());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task AuthenticateUser_WithErrorOnTokenGeneration_ShouldBeError()
        {
            // Arrange
            var validAndEnabledUser = new User();
            validAndEnabledUser.EnabledUser();
            validAndEnabledUser.SetHashedPassword("1234567");
            validAndEnabledUser.SetUserName("jose.c");

            var request = new AuthenticateUserRequest
            {
                HashedPassword = "123456",
                Username = "jose.c"
            };

            // Action
            var sut = new AuthenticateUserRequestHandler(_userRepository.Object, _tokenGeneratorService.Object);

            _userRepository.Setup(data => data.AuthenticateUser("jose.c"))
                .Returns(Result.Success(validAndEnabledUser));

            _tokenGeneratorService.Setup(token => token.GenerateToken(validAndEnabledUser))
                .Returns(Result.Error<AuthenticatedUserViewModel>(new Exception("Error on token generation")));

            var result = await sut.Handle(request, new CancellationToken());

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
        }
    }
}
