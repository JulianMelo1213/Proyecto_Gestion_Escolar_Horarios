using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class HorarioConfiguration : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> entity)
        {
                entity.HasKey(e => e.HorarioId).HasName("PK__Horarios__BB881B7E352A0829");

                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Aula).WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.AulaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Horarios__AulaId__37A5467C");

                entity.HasOne(d => d.Clase).WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.ClaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Horarios__ClaseI__36B12243");

                entity.HasOne(d => d.Dia).WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.DiaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Horarios__DiaId__38996AB5");
        }
    }
}
