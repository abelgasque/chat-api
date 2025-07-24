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

        private SecurityTokenDescriptor SetTokenDecriptor(string secret, Claim[] claims, DateTime expireIn)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expireIn,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private TokenResponse GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var accessTokenDescriptor = SetTokenDecriptor(
                _settings.Secret,
                claims,
                DateTime.UtcNow.AddMinutes(_settings.ExpireIn)
            );

            var refreshTokenDescriptor = SetTokenDecriptor(
                _settings.RefreshSecret,
                claims,
                DateTime.UtcNow.AddDays(_settings.ExpireDays)
            );

            var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
            var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

            return new TokenResponse()
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = tokenHandler.WriteToken(refreshToken),
                ExpiresIn = _settings.ExpireIn
            };
        }

        public async Task<TokenResponse> Login(TokenRequest pEntity)
        {
            UserModel user = await _service.ReadByMail(pEntity.Username)
                ?? throw new UnauthorizedException("Invalid credentials.");

            if (!user.ActiveAt.HasValue)
                throw new UnauthorizedException("User inactive!") { };


            if (user.BlockedAt.HasValue)
                throw new UnauthorizedException("User blocked!") { };

            if (!user.Password.Equals(pEntity.Password))
                throw new UnauthorizedException("Invalid password!") { };

            user.NuLogged += 1;
            user.LoggedAt = DateTime.UtcNow;
            await _service.UpdateAsync(user);

            return GenerateToken(user);
        }

        public async Task<TokenResponse> Refresh(RefreshTokenRequest pEntity)
        {
            var refreshTokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.RefreshSecret)),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(pEntity.RefreshToken, refreshTokenValidationParameters, out _);
            }
            catch
            {
                throw new UnauthorizedException("Invalid refresh token!");
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException("Invalid token payload.");
            }

            var user = await _service.ReadById(Guid.Parse(userId));
            user.NuRefreshed += 1;
            user.RefreshedAt = DateTime.UtcNow;
            await _service.UpdateAsync(user);

            return GenerateToken(user);
        }
    }
}
