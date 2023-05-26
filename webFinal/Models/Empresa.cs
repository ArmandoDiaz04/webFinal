using System.ComponentModel.DataAnnotations;

namespace webFinal.Models
{
    public class Empresa
    {
        [Key]
        public int IdEmpresa { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int NumeroEmpleados { get; set; }

        [StringLength(100)]
        public string Rubro { get; set; }

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

        public List<Publicacion> Publicaciones { get; set; }
        public List<ValoracionEmpresa> ValoracionesEmpresas { get; set; }
    }
}
