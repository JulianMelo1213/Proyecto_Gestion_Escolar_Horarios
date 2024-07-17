using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class ClaseConfiguration : IEntityTypeConfiguration<Clase>
    {
        public void Configure(EntityTypeBuilder<Clase> entity)
        {
         
                entity.HasKey(e => e.ClaseId).HasName("PK__Clases__F542955360233BDC");

                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.Profesor).WithMany(p => p.Clases)
                    .HasForeignKey(d => d.ProfesorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Clases__Profesor__2D27B809");
           
        }
    }
}
