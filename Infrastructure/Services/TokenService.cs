using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatApi.Infrastructure.Exceptions;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Entities.Settings;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Domain.Responses;
using ChatApi.Domain.Requests;

namespace ChatApi.Infrastructure.Services
{
    public class TokenService
    {
        private readonly ApplicationSettings _settings;

        private readonly UserService _service;

        public TokenService(
            IOptions<ApplicationSettings> settings,
            UserService service
        )
        {
            _settings = settings.Value;
            _service = service;
        }

        private TokenResponse GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpireIn),
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenResponse()
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = _settings.ExpireIn
            };
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (
                securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
            ) throw new UnauthorizedException("Invalid token!");

            return principal;
        }

        public async Task<TokenResponse> Login(TokenRequest pEntity)
        {
            UserModel user = await _service.ReadByMail(pEntity.Username);

            user.NuLogged += 1;
            await _service.UpdateAsync(user);

            if (!user.Password.Equals(pEntity.Password))
            {
                throw new UnauthorizedException("Invalid password!") { };
            }

            var token = GenerateToken();
            token.Data = new UserResponse(user) { };
            return token;
        }

        public async Task<TokenResponse> Refresh(RefreshTokenRequest pEntity)
        {
            UserModel user = await _service.ReadById(pEntity.Id);
            var principal = GetPrincipalFromExpiredToken(pEntity.AccessToken);

            if (principal is null)
            {
                throw new BadRequestException("Invalid access token or refresh token!");
            }

            var token = GenerateToken();
            token.Data = new UserResponse(user) { };
            return token;
        }
    }
}
