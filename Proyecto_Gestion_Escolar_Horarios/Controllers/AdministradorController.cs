using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Usuario;
using Proyecto_Gestion_Escolar_Horarios.Helper;
using Proyecto_Gestion_Escolar_Horarios.Models;
using Proyecto_Gestion_Escolar_Horarios.Services.TokenServices;
using System.Data.Entity;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Authorize(Policy = "RequiereRolAdministrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly UserManager<Usuario> userManager;
        private readonly ITokenService tokenService;
        private readonly SignInManager<Usuario> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdministradorController(UserManager<Usuario> userManager, ITokenService tokenService, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        [HttpPost("añadir-rol")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AñadirRol([FromBody] CambiarRolDTO cambiarRolDTO)
        {
            try
            {
                var user = await userManager.FindByIdAsync(cambiarRolDTO.IdUsuario);

                if (user == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                foreach (var rol in cambiarRolDTO.Roles)
                {
                    var rolExiste = await roleManager.RoleExistsAsync(rol);
                    if (!rolExiste)
                    {
                        return BadRequest($"El rol '{rol}' no existe");
                    }
                }

                var currentRoles = await userManager.GetRolesAsync(user);
                var roleResult = await userManager.AddToRolesAsync(user, cambiarRolDTO.Roles);

                if (!roleResult.Succeeded)
                {
                    return StatusCode(500, roleResult.Errors);
                }

                return Ok("Roles añadidos exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost("quitar-rol")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> QuitarRol([FromBody] CambiarRolDTO cambiarRolDTO)
        {
            try
            {
                var user = await userManager.FindByIdAsync(cambiarRolDTO.IdUsuario);

                if (user == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                var currentRoles = await userManager.GetRolesAsync(user);
                var roleResult = await userManager.RemoveFromRolesAsync(user, cambiarRolDTO.Roles);

                if (!roleResult.Succeeded)
                {
                    return StatusCode(500, roleResult.Errors);
                }

                return Ok("Roles eliminados exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("roles")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<IEnumerable<string>>> GetRoles()
        {
            var roles = await roleManager.Roles.Select(r => r.Name).ToListAsync();
            return Ok(roles);
        }
    }
}

