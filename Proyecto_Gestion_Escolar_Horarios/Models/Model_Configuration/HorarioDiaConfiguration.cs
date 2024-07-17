using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class HorarioDiaConfiguration : IEntityTypeConfiguration<HorarioDia>
    {
        public void Configure(EntityTypeBuilder<HorarioDia> entity)
        {
          
                entity.HasKey(e => e.HorarioDiaId).HasName("PK__HorarioD__C21830CAB5EA53D3");

                entity.HasOne(d => d.Dia).WithMany(p => p.HorarioDia)
                    .HasForeignKey(d => d.DiaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HorarioDi__DiaId__3D5E1FD2");

                entity.HasOne(d => d.Horario).WithMany(p => p.HorarioDia)
                    .HasForeignKey(d => d.HorarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HorarioDi__Horar__3C69FB99");
           
        }
    }
}
