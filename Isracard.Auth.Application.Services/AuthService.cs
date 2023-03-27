using AutoMapper;
using Isracard.Auth.Application.Services.Dtos;
using Isracard.Auth.Domain.Core.Models;
using Isracard.Auth.Domain.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Isracard.Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILoginRepository repository;
        private readonly ILogger logger;
        private readonly IHttpContextAccessor httpContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public AuthService(IConfiguration configuration, IMapper mapper, ILoginRepository repository, ILogger<AuthService> logger, IHttpContextAccessor httpContext )
        {
            this.repository = repository;
            this.logger = logger;
            this.httpContext = httpContext;
            this.mapper = mapper;
            this.configuration = configuration;
        }

       
        public async Task AddLoginValue(LoginDTO value)
        {
            
            await  repository.AddLoginValue(mapper.Map<Login>(value));
        }

        public async Task<List<LoginDTO>> GetLoginValues()
        {
           var result = await repository.GetLoginValues();

           return mapper.Map<List<LoginDTO>>(result);
        }

        public async Task DeleteLoginValue(Guid id)
        {
           await repository.DeleteLoginValue(id);
        }

        public async Task<string> LogIn(LoginDTO login)
        {
            var tokenAsString = string.Empty;
            LogClaims();

            var loginResult = false;
            var result = await repository.GetLoginByUserName(login.UserName);
            // MD5 Hash validation

            if (result != null && login.Password == result.Password)
            {
                loginResult = true;

                var claims = new[]
                        {
                        new Claim("Email", login.UserName),
                        new Claim(ClaimTypes.NameIdentifier, login.UserName)
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:key"]));
                var token = new JwtSecurityToken(
                    issuer: configuration["AuthSettings:Issuer"],
                    audience: configuration["AuthSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );
                tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            }

            return tokenAsString;
        }


        private void LogClaims() => logger.LogInformation(message: string.Join(',', httpContext.HttpContext.User.Claims));
    }

}
