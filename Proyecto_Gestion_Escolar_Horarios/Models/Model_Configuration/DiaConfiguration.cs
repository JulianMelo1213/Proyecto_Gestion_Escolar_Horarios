using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class DiaConfiguration : IEntityTypeConfiguration<Dia>
    {
        public void Configure(EntityTypeBuilder<Dia> entity)
        {
           
                entity.HasKey(e => e.DiaId).HasName("PK__Dias__ED194C767FD12FCD");

                entity.HasIndex(e => e.Nombre, "UQ__Dias__75E3EFCFB07ED18C").IsUnique();

                entity.Property(e => e.Nombre).HasMaxLength(50);
            
        }
    }
}
