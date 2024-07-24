using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Usuario;
using Proyecto_Gestion_Escolar_Horarios.Helper;
using Proyecto_Gestion_Escolar_Horarios.Models;
using Proyecto_Gestion_Escolar_Horarios.Services.TokenServices;
using System.Security.Claims;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> userManager;
        private readonly ITokenService tokenService;
        private readonly SignInManager<Usuario> signInManager;

        public UsuarioController(UserManager<Usuario> userManager, ITokenService tokenService, SignInManager<Usuario> signInManager)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
        }


        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await userManager.FindByNameAsync(loginDTO.NombreUsuario);
            if (usuario != null || await userManager.CheckPasswordAsync(usuario, loginDTO.Password))
            {
                await signInManager.SignInAsync(usuario, isPersistent: false);
                var roles = await userManager.GetRolesAsync(usuario);
                var claims = ClaimsHelper.GenerateClaims(usuario, roles);
                var token = tokenService.GenerateJWTToken(claims);
                var refreshToken = await tokenService.StoreRefreshTokenAsync(usuario);

                return Ok(new TokenDTO
                {
                    Token = token,
                    RefreshToken = refreshToken
                });
            }
            return Unauthorized("Usuario no encontrado y/o contraseña incorrecta");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (usuarioId == null)
            {
                return BadRequest("No se pudo encontrar el ID de usuario en el token JWT.");
            }

            var usuario = await userManager.FindByIdAsync(usuarioId);

            if (usuario == null)
            {
                return BadRequest("Usuario no encontrado.");
            }

            tokenService.RemoveRefreshTokenAsync(usuario);
            await signInManager.SignOutAsync();

            return Ok("Sesión cerrada correctamente.");
        }
    }
}
