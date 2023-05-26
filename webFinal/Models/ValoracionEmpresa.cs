using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webFinal.Models
{
    public class ValoracionEmpresa
    {
        [Key]
        public int IdValoracion { get; set; }

        public int IdEmpresa { get; set; }
        public int IdUsuario { get; set; }

        [Column(TypeName = "ntext")]
        public string Comentario { get; set; }

        public int Calificacion { get; set; }

        public DateTime FechaValoracion { get; set; }

        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
