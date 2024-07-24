using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Usuario
{
    public class LoginDTO
    {
        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
