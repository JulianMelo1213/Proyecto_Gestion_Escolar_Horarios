using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Proyecto_Gestion_Escolar_Horarios.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto_Gestion_Escolar_Horarios.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<Usuario> _userManager;

        public TokenService(IConfiguration config, UserManager<Usuario> userManager)
        {
            _config = config;
            var signingKey = _config["JWT:SigningKey"];
            if (string.IsNullOrEmpty(signingKey))
            {
                throw new ArgumentException("No se puede encontrar la Signing key definida en la configuración");
            }
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            _userManager = userManager;
        }

        public string GenerateJWTToken(IList<Claim> claims)
        {
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }
                return principal;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Invalid token", ex);
            }
        }

        public async Task<string> StoreRefreshTokenAsync(Usuario user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationTime = DateTime.UtcNow.AddMinutes(30);

            await _userManager.UpdateAsync(user);

            return refreshToken;
        }

        public async Task<bool> RemoveRefreshTokenAsync(Usuario user)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpirationTime = DateTime.MinValue;

            await _userManager.UpdateAsync(user);
            return true;
        }
    }
}
