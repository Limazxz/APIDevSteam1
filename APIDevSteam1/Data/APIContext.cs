using APIDevSteam1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIDevSteam1.Data
{
    public class APIContext : IdentityDbContext<Usuario>
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }

        //Dbset
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<JogoCategoria> JogosCategorias { get; set; }
        public DbSet<JogoMidia> JogosMidia { get; set; }
        public DbSet<Carrinho> Carinhos { get; set; }
        public DbSet<ItemCarrinho> ItensCarrinhos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // tabelas
            builder.Entity<Jogo>().ToTable("Jogos");
            builder.Entity<Categoria>().ToTable("Categorias");
            builder.Entity<JogoCategoria>().ToTable("JogosCategorias");
            builder.Entity<JogoMidia>().ToTable("JogosMidia");
            builder.Entity<Carrinho>().ToTable("Carinhos");
            builder.Entity<ItemCarrinho>().ToTable("ItensCarrinho");

        }
    }
}
