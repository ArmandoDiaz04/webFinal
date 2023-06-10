using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webFinal.Models
{
    [Table("ValoracionesEmpresas")]
    public class ValoracionEmpresa
    {
        [Key]
        [Column("IdValoracion")]
        public int IdValoracion { get; set; }

        [Column("IdEmpresa")]
        public int? IdEmpresa { get; set; }

        [Column("IdUsuario")]
        public int? IdUsuario { get; set; }

        [Column("Comentario")]
        public string Comentario { get; set; }

        [Column("Calificacion")]
        public int? Calificacion { get; set; }

        [Column("FechaValoracion")]
        public DateTime? FechaValoracion { get; set; }

        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
