using AuthenticationService.Application.Request;
using AuthenticationService.Domain.Repository;
using AuthenticationService.Domain.Services;
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
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthenticateUserRequestHandler(IUserRepository userRepository, ITokenGeneratorService tokenGeneratorService)
        {
            _userRepository = userRepository;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<Result<AuthenticatedUserViewModel>> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var (isUserSearchSuccess, userSearched, userSearchExecption)
                = await _userRepository.AuthenticateUser(username: request.Username)
                .ConfigureAwait(false);

            if (!isUserSearchSuccess || userSearched is null)
                return Result.Error<AuthenticatedUserViewModel>(userSearchExecption);

            if (!userSearched.Enabled)
                return Result.Error<AuthenticatedUserViewModel>(new Exception("User disabled"));

            if (!userSearched.HashedPassword.Equals(request.HashedPassword))
                return Result.Error<AuthenticatedUserViewModel>(new Exception("Error user/password incorrect"));

            var (isGeneratedToken, tokenResult, tokenException) = await _tokenGeneratorService.GenerateToken(userSearched).ConfigureAwait(false);

            if (!isGeneratedToken)
                return Result.Error<AuthenticatedUserViewModel>(tokenException);

            return Result.Success(tokenResult);
        }
    }
}
