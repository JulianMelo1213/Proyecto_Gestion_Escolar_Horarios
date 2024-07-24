using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.Models
{
    public class Reporte
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

    }
}
