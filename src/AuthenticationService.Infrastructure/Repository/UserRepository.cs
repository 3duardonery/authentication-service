using AuthenticationService.Domain.Models;
using AuthenticationService.Domain.Repository;
using OperationResult;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure.Repository
{
    public sealed class UserRepository : IUserRepository
    {
        public Task<Result<User>> AuthenticateUser(string username)
        {
            return null;
        }
    }
}
