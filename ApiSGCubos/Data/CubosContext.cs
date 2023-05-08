using ApiSGCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSGCubos.Data
{
    public class CubosContext : DbContext
    {

        public CubosContext
          (DbContextOptions<CubosContext> options)
          : base(options) { }
        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<CompraCubos> Pedidos { get; set; }
    }
}
