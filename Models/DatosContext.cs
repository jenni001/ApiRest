using Microsoft.EntityFrameworkCore;

namespace ApiRest.Models
{
    public class DatosContext : DbContext
    {
        public DatosContext(DbContextOptions<DatosContext> options)
            : base(options)
        {
        }

        public DbSet<Datos> Datos { get; set; }
    }
}