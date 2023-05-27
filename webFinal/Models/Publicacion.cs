using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webFinal.Models
{

    public class Publicacion
    {
        [Key]
        public int IdPublicacion { get; set; }

        public int IdEmpresa { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Column(TypeName = "ntext")]
        public string Descripcion { get; set; }

        public DateTime FechaPublicacion { get; set; }

        [ForeignKey("IdEmpresa")]
        public Empresa Empresa { get; set; }

        public string NombreEmpresa { get; set; }
    }


}
