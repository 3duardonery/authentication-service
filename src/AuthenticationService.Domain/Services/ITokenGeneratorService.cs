using AuthenticationService.Domain.Models;
using AuthenticationService.Shared.ViewModels;
using OperationResult;
using System.Threading.Tasks;

namespace AuthenticationService.Domain.Services
{
    public interface ITokenGeneratorService
    {
        Task<Result<AuthenticatedUserViewModel>> GenerateToken(User user);
    }
}
