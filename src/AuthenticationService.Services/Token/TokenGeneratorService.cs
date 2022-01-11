using AuthenticationService.Domain.Models;
using AuthenticationService.Domain.Services;
using AuthenticationService.Shared.ViewModels;
using Microsoft.IdentityModel.Tokens;
using OperationResult;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Services.Token
{
    public sealed class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly string _hashKey;

        public TokenGeneratorService(string hashKey) => _hashKey = hashKey;

        public Task<Result<AuthenticatedUserViewModel>> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_hashKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("Id", user.Id.ToString()),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

            return Result.Success(new AuthenticatedUserViewModel 
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.Username
            });
        }
    }
}
