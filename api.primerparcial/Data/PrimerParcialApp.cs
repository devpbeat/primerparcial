using api.primerparcial.Models;
using Microsoft.EntityFrameworkCore;
namespace api.primerparcial.Data
{
    public class PrimerParcialApp : DbContext
    {
        public PrimerParcialApp(DbContextOptions<PrimerParcialApp> options) : base(options)
        { 
        }
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Ciudad> Ciudades => Set<Ciudad>();
    }
}
