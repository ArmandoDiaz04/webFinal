using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webFinal.Models
{
    public class Postulacion
    {
        [Key]
       
        public int IdPostulacion { get; set; }

        public int IdUsuario { get; set; }

        public int IdEmpresa { get; set; }

        public int IdPublicacion { get; set; }

        public DateTime FechaPostulacion { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa { get; set; }

        [ForeignKey("IdPublicacion")]
        public virtual Publicacion Publicacion { get; set; }

    }
}
