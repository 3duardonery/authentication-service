using AuthenticationService.Domain.Models;
using AuthenticationService.Domain.Services;
using AuthenticationService.Shared.ViewModels;
using OperationResult;
using System;
using System.Threading.Tasks;

namespace AuthenticationService.Services.Token
{
    public sealed class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly string _hashKey;

        public TokenGeneratorService(string hashKey)
        {
            _hashKey = hashKey;
        }

        public Task<Result<AuthenticatedUserViewModel>> GenerateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
