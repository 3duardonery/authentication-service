using AuthenticationService.Application.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthenticationService.IoC
{
    public static class MediatRDependencyInjection
    {
        public static void AddMediatRDependency(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(AuthenticateUserRequestHandler).GetTypeInfo().Assembly);
        }
    }
}
