using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ChatApi.Domain.Entities.DTO;
using ChatApi.Domain.Entities.Exceptions;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Entities.Settings;
using System.Text;
using System.Threading.Tasks;

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

        private TokenDTO GenerateToken()
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
            return new TokenDTO()
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

        public async Task<TokenDTO> Login(UserDTO pEntity)
        {
            UserModel user = await _service.ReadByMail(pEntity.Username);

            // if ((!customer.Active) || (customer.Block))
            // {
            //     throw new UnauthorizedException("User blocked or inactive!") { };
            // }

            // if (customer.AuthAttempts >= _settings.AuthAttempts)
            // {
            //     customer.SetBlock(true);
            //     await _service.UpdateAsync(customer);
            //     throw new UnauthorizedException("User blocked temporarily!") { };
            // }

            if (!user.Password.Equals(pEntity.Password))
            {
                user.AuthAttempts += 1;
                await _service.UpdateAsync(user);
                throw new UnauthorizedException("Invalid password!") { };
            }

            user.AuthAttempts = 0;
            await _service.UpdateAsync(user);

            var token = GenerateToken();
            //token.Customer = new CustomerDTO(customer) { };
            return token;
        }

        public async Task<TokenDTO> Refresh(RefreshTokenDTO pEntity)
        {
            UserModel user = await _service.ReadById(pEntity.Id);
            var principal = GetPrincipalFromExpiredToken(pEntity.AccessToken);

            if (principal is null)
            {
                throw new BadRequestException("Invalid access token or refresh token!");
            }

            var token = GenerateToken();
            //token.Customer = new CustomerDTO(user) { };
            return token;
        }
    }
}
