using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration;

namespace Proyecto_Gestion_Escolar_Horarios.Models;

public partial class GestionEstudiantesContext : IdentityDbContext<Usuario>
{
    public GestionEstudiantesContext()
    {

    }

    public GestionEstudiantesContext(DbContextOptions<GestionEstudiantesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aula> Aulas { get; set; }

    public virtual DbSet<Clase> Clases { get; set; }

    public virtual DbSet<Dia> Dias { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<HorarioAsignatura> HorarioAsignaturas { get; set; }

    public virtual DbSet<HorarioDia> HorarioDias { get; set; }

    public virtual DbSet<Inscripciones> Inscripciones { get; set; }

    public virtual DbSet<Profesores> Profesores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AulaConfiguration());
        modelBuilder.ApplyConfiguration(new ClaseConfiguration());
        modelBuilder.ApplyConfiguration(new DiaConfiguration());
        modelBuilder.ApplyConfiguration(new EstudianteConfiguration());
        modelBuilder.ApplyConfiguration(new HorarioConfiguration());
        modelBuilder.ApplyConfiguration(new HorarioAsignaturaConfiguration());
        modelBuilder.ApplyConfiguration(new HorarioDiaConfiguration());
        modelBuilder.ApplyConfiguration(new InscripcionesConfiguration());
        modelBuilder.ApplyConfiguration(new ProfesoresConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
