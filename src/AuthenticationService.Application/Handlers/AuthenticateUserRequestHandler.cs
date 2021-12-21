using AuthenticationService.Application.Request;
using AuthenticationService.Domain.Repository;
using AuthenticationService.Shared.ViewModels;
using MediatR;
using OperationResult;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Handlers
{
    public class AuthenticateUserRequestHandler
        : IRequestHandler<AuthenticateUserRequest, Result<AuthenticatedUserViewModel>>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<AuthenticatedUserViewModel>> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var (isUserSearchSuccess, userSearched, userSearchExecption)
                = await _userRepository.AuthenticateUser(username: request.Username)
                .ConfigureAwait(false);

            if (!isUserSearchSuccess || userSearched is null)
                return Result.Error<AuthenticatedUserViewModel>(new Exception("Error on user search"));

            if (!userSearched.Enabled)
                return Result.Error<AuthenticatedUserViewModel>(new Exception("User disabled"));

            if (!userSearched.HashedPassword.Equals(request.HashedPassword))
                return Result.Error<AuthenticatedUserViewModel>(new Exception("Error user/password incorrect"));

            return Result.Success(new AuthenticatedUserViewModel { Token = "123456", Username = "jose.c" });
        }
    }
}
