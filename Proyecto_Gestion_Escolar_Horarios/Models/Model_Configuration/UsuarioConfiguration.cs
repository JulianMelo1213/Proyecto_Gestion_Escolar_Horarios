using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Proyecto_Gestion_Escolar_Horarios.Models.Model_Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entity)
        {
            
                entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B8F41AD9F3");

                entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534E1E7F0E1").IsUnique();

                entity.Property(e => e.Contraseña).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Rol).HasMaxLength(50);
            
        }
    }
}
