namespace Proyecto_Gestion_Escolar_Horarios.DTO.Usuario
{
    public class NuevoUsuarioDTO
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        public string? NombreUsuario { get; set; }

        public string? Correo { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }
    }
}
