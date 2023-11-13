using Consultorio_Seguros.Models;
using Microsoft.EntityFrameworkCore;

namespace Consultorio_Seguros.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes {  get; set; }
        public DbSet<Seguro> Seguros { get; set; }
        public DbSet<Asegurado> Asegurados {  get; set; }
    }
}
