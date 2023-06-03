using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webFinal.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "ntext")]
        public string InformacionLaboral { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        [StringLength(100)]
        public string Ciudad { get; set; }

        [StringLength(100)]
        public string Estado { get; set; }

        [StringLength(100)]
        public string Pais { get; set; }
        public int idrol { get; set; }

        public string contrasenia { get; set; }
      //  public List<ValoracionEmpresa> ValoracionesEmpresas { get; set; }
    }
}
