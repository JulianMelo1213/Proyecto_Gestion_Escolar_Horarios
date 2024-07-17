using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class AulaConfiguration : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> entity)
        {
                entity.HasKey(e => e.AulaId).HasName("PK__Aulas__A8529BF8DB5BFD62");

                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Nombre).HasMaxLength(100);
           
        }
    }
}
