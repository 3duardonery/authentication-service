using AuthenticationService.Domain.Repository;
using AuthenticationService.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationService.IoC
{
    public static class AuthenticationServiceDepenciesInjection
    {
        public static void AddProjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
