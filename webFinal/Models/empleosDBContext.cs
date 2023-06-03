using Microsoft.EntityFrameworkCore;

namespace webFinal.Models
{
    public class empleosDBContext : DbContext
    {
        public empleosDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<ValoracionEmpresa> ValoracionesEmpresas { get; set; }
        public DbSet<Postulacion> postulaciones { get; set; }
    }

}
