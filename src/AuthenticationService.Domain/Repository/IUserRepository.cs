using AuthenticationService.Domain.Models;
using OperationResult;
using System.Threading.Tasks;

namespace AuthenticationService.Domain.Repository
{
    public interface IUserRepository
    {
        Task<Result<User>> AuthenticateUser(string username);
    }
}
