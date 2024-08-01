using Proyecto_Gestion_Escolar_Horarios.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Proyecto_Gestion_Escolar_Horarios.Helper
{
    public class ClaimsHelper
    {
        public static List<Claim> GenerateClaims(Usuario user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.Nombre),
                new Claim(ClaimTypes.Surname, user.Apellido),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
