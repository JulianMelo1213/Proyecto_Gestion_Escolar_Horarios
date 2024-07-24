using Microsoft.AspNetCore.Identity;
using Proyecto_Gestion_Escolar_Horarios.Models;
using Proyecto_Gestion_Escolar_Horarios.Services.TokenServices;

namespace Proyecto_Gestion_Escolar_Horarios.Middleware
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RefreshTokenMiddleware> _logger;

        public RefreshTokenMiddleware(RequestDelegate next, IServiceProvider serviceProvider, ILogger<RefreshTokenMiddleware> logger)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(accessToken))
            {
                await _next(context);
                return;
            }

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();

                    var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
                    if (principal != null)
                    {
                        var nombreUsuario = principal.Identity.Name;
                        var usuario = await userManager.FindByNameAsync(nombreUsuario);
                        if (usuario != null && usuario.RefreshToken == null)
                        {
                            await tokenService.StoreRefreshTokenAsync(usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Un excepción no ha sido manejada durante el refresco del token.");
            }

            await _next(context);
        }
    }
}