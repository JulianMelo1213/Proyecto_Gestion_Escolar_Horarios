using Proyecto_Gestion_Escolar_Horarios.Models;
using System.Security.Claims;

namespace Proyecto_Gestion_Escolar_Horarios.Services.TokenServices
{
    public interface ITokenService
    {
        string GenerateJWTToken(IList<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<string> StoreRefreshTokenAsync(Usuario user);
        Task<bool> RemoveRefreshTokenAsync(Usuario user);
    }
}
