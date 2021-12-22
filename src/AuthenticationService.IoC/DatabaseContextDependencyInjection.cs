using AuthenticationService.Shared.ValueObject;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace AuthenticationService.IoC
{
    public static class DatabaseContextDependencyInjection
    {
        public static void AddDatabaseDependency(this IServiceCollection services, DatabaseSettings databaseSettings) 
        {
            services.AddScoped<IMongoClient>(x => new MongoClient(databaseSettings.ConnectionString));
        }
    }
}
