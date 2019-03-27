using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebApiPegoPaque.Models;

namespace WebApiPegoPaque.Util
{
    public class DataContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Usuario> DbUsuarios { get; set; }
        public virtual DbSet<Produto> DbProtudos { get; set; }
        public virtual DbSet<Marca> DbMarcas { get; set; }
        public virtual DbSet<CategoriaMarca> DbCategoriaMarca { get; set; }
    }
}
