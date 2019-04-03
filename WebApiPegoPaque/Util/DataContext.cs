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
        public virtual DbSet<Produto> DbProdutos { get; set; }
        public virtual DbSet<Marca> DbMarcas { get; set; }
        public virtual DbSet<CategoriaMarca> DbCategoriaMarca { get; set; }
        public virtual DbSet<TipoVolume> DbTipoVolumes { get; set; }
        public virtual DbSet<TipoVolumesProduto> DbTipoVolumesProdutos { get; set; }
        public virtual DbSet<Estabelecimento> DbEstabelecimentos { get; set; }
        public virtual DbSet<ListaModelo> DbListasModelo { get; set; }
        public virtual DbSet<ProdutosLista> DbProdutosLista { get; set; }
    }
}
