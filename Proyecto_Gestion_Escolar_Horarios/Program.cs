using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Proyecto_Gestion_Escolar_Horarios.Helper;
using Proyecto_Gestion_Escolar_Horarios.Middleware;
using Proyecto_Gestion_Escolar_Horarios.Models;
using Proyecto_Gestion_Escolar_Horarios.Services.AulaServices;
using Proyecto_Gestion_Escolar_Horarios.Services.ClaseServices;
using Proyecto_Gestion_Escolar_Horarios.Services.DiaServices;
using Proyecto_Gestion_Escolar_Horarios.Services.EstudianteServices;
using Proyecto_Gestion_Escolar_Horarios.Services.HorarioAsignaturaServices;
using Proyecto_Gestion_Escolar_Horarios.Services.HorarioDiaServices;
using Proyecto_Gestion_Escolar_Horarios.Services.HorarioServices;
using Proyecto_Gestion_Escolar_Horarios.Services.InscripcionesServices;
using Proyecto_Gestion_Escolar_Horarios.Services.ProfesoresServices;
using Proyecto_Gestion_Escolar_Horarios.Services.ReporteServices;
using Proyecto_Gestion_Escolar_Horarios.Services.TokenServices;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GestionEstudiantesContext>(db => db.UseSqlServer(connectionString));
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(builder.Configuration["JWT:SigningKey"])
            ),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization(options =>
{
    PoliciesHelper.AddPolicies(options);
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Favor añadir un token válido",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IAulaService, AulaService>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IProfesoresService, ProfesoresService>();
builder.Services.AddScoped<IDiaService, DiaService>();
builder.Services.AddScoped<IInscripcionesService, InscripcionesService>();
builder.Services.AddScoped<IHorarioService, HorarioService>();
builder.Services.AddScoped<IClaseService, ClaseService>();
builder.Services.AddScoped<IHorarioAsignaturaService, HorarioAsignaturaService>();
builder.Services.AddScoped<IHorarioDiaService, HorarioDiaService>();
builder.Services.AddScoped<UserManager<Usuario>>();
builder.Services.AddScoped<SignInManager<Usuario>>();
builder.Services.AddScoped<ClaimsHelper>();
builder.Services.AddScoped<IReporteService, ReporteService>();
// builder.Services.AddScoped<ITokenService, TokenService>();


// Configurar Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<GestionEstudiantesContext>()
    .AddDefaultTokenProviders();


//Permitir acceso desde el frontend

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("AllowAllOrigins", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseAuthorization();
// app.UseAuthentication();

app.UseMiddleware<RefreshTokenMiddleware>();

app.MapControllers();

app.Run();
