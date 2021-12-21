using AuthenticationService.Shared.ViewModels;
using MediatR;
using OperationResult;

namespace AuthenticationService.Application.Request
{
    public sealed class AuthenticateUserRequest : IRequest<Result<AuthenticatedUserViewModel>>
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
    }
}
