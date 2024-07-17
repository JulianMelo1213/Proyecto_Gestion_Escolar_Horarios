using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class InscripcionesConfiguration : IEntityTypeConfiguration<Inscripciones>
    {
        public void Configure(EntityTypeBuilder<Inscripciones> entity)
        {
          
                entity.HasKey(e => e.InscripcionId).HasName("PK__Inscripc__168316B9214670C7");

                entity.HasIndex(e => new { e.EstudianteId, e.ClaseId }, "UK_Inscripciones").IsUnique();

                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Clase).WithMany(p => p.Inscripciones)
                    .HasForeignKey(d => d.ClaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inscripci__Clase__47DBAE45");

                entity.HasOne(d => d.Estudiante).WithMany(p => p.Inscripciones)
                    .HasForeignKey(d => d.EstudianteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inscripci__Estud__46E78A0C");
            
        }
    }
}
