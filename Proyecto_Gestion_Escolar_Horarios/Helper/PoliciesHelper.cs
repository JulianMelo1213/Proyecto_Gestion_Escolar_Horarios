using Microsoft.AspNetCore.Authorization;

namespace Proyecto_Gestion_Escolar_Horarios.Helper
{
    public class PoliciesHelper
    {
        public static void AddPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("RequiereRolAdministrador", policy => policy.RequireRole("Administrador"));
            options.AddPolicy("RequiereRolEstudiante", policy => policy.RequireRole("Estudiante"));
            options.AddPolicy("RequiereRolProfesor", policy => policy.RequireRole("Profesor"));
        }
    }
}