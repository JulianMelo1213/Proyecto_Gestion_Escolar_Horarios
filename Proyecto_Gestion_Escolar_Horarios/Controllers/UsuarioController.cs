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
        private readonly RoleManager<IdentityRole> roleManager;

        public UsuarioController(UserManager<Usuario> userManager, ITokenService tokenService, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDTO registroDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var usuario = new Usuario
                {
                    UserName = registroDTO.NombreUsuario,
                    Nombre = registroDTO.Nombre,
                    Apellido = registroDTO.Apellido,
                    Email = registroDTO.CorreoElectronico,
                };

                var usuarioCreado = await userManager.CreateAsync(usuario, registroDTO.Password);
                if (usuarioCreado.Succeeded)
                {
                    var rolExiste = await roleManager.RoleExistsAsync(registroDTO.Rol);
                    if (!rolExiste)
                    {
                        return BadRequest("El rol especificado no existe");
                    }

                    var roleResult = await userManager.AddToRoleAsync(usuario, registroDTO.Rol);
                    if (roleResult.Succeeded)
                    {
                        var roles = await userManager.GetRolesAsync(usuario);
                        var claims = ClaimsHelper.GenerateClaims(usuario, roles);
                        var token = tokenService.GenerateJWTToken(claims);
                        usuario.Token = token;
                        await userManager.UpdateAsync(usuario);
                        var refreshToken = await tokenService.StoreRefreshTokenAsync(usuario);

                        return Ok(new NuevoUsuarioDTO
                        {
                            NombreUsuario = usuario.UserName,
                            Nombre = usuario.Nombre,
                            Apellido = usuario.Apellido,
                            Correo = usuario.Email,
                            Token = token,
                            RefreshToken = refreshToken
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, usuarioCreado.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
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
