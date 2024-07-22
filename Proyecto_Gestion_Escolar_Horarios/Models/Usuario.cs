using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyecto_Gestion_Escolar_Horarios.Models;

public partial class Usuario : IdentityUser
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    [JsonIgnore]
    public string? Token { get; set; }
    [JsonIgnore]
    public string? RefreshToken { get; set; }
    [JsonIgnore]
    public DateTime RefreshTokenExpirationTime { get; set; }
    public DateTime? FechaRegistro { get; set; }
}
