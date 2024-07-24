using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.Models;
using Proyecto_Gestion_Escolar_Horarios.Services.AulaServices;
using Proyecto_Gestion_Escolar_Horarios.Services.DiaServices;
using Proyecto_Gestion_Escolar_Horarios.Services.EstudianteServices;
using Proyecto_Gestion_Escolar_Horarios.Services.InscripcionesServices;
using Proyecto_Gestion_Escolar_Horarios.Services.ProfesoresServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GestionEstudiantesContext>(db => db.UseSqlServer(connectionString));
builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IAulaService, AulaService>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IProfesoresService, ProfesoresService>();
builder.Services.AddScoped<IDiaService, DiaService>();
builder.Services.AddScoped<IInscripcionesService, InscripcionesService>();

// Configurar Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<GestionEstudiantesContext>()
    .AddDefaultTokenProviders();

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

app.MapControllers();

app.Run();
