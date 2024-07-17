using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class HorarioAsignaturaConfiguration : IEntityTypeConfiguration<HorarioAsignatura>
    {
        public void Configure(EntityTypeBuilder<HorarioAsignatura> entity)
        {
           
                entity.HasKey(e => e.HorarioAsignaturaId).HasName("PK__HorarioA__C17169C99D79096B");

                entity.ToTable("HorarioAsignatura");

                entity.HasOne(d => d.Dia).WithMany(p => p.HorarioAsignaturas)
                    .HasForeignKey(d => d.DiaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HorarioAs__DiaId__412EB0B6");

                entity.HasOne(d => d.Horario).WithMany(p => p.HorarioAsignaturas)
                    .HasForeignKey(d => d.HorarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HorarioAs__Horar__403A8C7D");

                entity.HasOne(d => d.Profesor).WithMany(p => p.HorarioAsignaturas)
                    .HasForeignKey(d => d.ProfesorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HorarioAs__Profe__4222D4EF");
           
        }
    }
}
