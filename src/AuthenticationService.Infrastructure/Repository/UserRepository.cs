using AuthenticationService.Domain.Models;
using AuthenticationService.Domain.Repository;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationService.Infrastructure.Repository
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("auth_api");
            _users = database.GetCollection<User>("users");
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
        }

        public async Task<Result<User>> AuthenticateUser(string username)
        {
            try
            {
                var filters = new List<FilterDefinition<User>>();

                filters.Add(Builders<User>.Filter.Eq(c => c.Username, username));

                var complexFilter = Builders<User>.Filter.And(filters);

                var response = await _users.FindAsync(complexFilter).ConfigureAwait(false);

                var user = response.FirstOrDefault();

                return Result.Success(user);
            }
            catch (Exception exception)
            {
                return Result.Error<User>(exception);
            }
        }
    }
}
