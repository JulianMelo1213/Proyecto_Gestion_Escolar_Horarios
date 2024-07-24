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
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Nombre + " " + user.Apellido),
                new(ClaimTypes.GivenName, user.UserName),
                new(JwtRegisteredClaimNames.Email, user.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
