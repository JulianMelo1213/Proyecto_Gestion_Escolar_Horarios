using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class ProfesoresConfiguration : IEntityTypeConfiguration<Profesores>
    {
        public void Configure(EntityTypeBuilder<Profesores> entity)
        {
            
                entity.HasKey(e => e.ProfesorId).HasName("PK__Profesor__4DF3F0C8946A89C7");

                entity.HasIndex(e => e.Email, "UQ__Profesor__A9D10534CC7F3E50").IsUnique();

                entity.Property(e => e.Apellido).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Nombre).HasMaxLength(100);
           
        }
    }
}
