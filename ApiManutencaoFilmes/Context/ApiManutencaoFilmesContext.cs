using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiManutencaoFilmes.Context {
    public class ApiManutencaoFilmesContext : IdentityDbContext
    {
        public ApiManutencaoFilmesContext (DbContextOptions<ApiManutencaoFilmesContext> options)
            : base(options) {
        }

        public DbSet<ApiManutencaoFilmes.Models.Genero> Generos { get; set; }
        public DbSet<ApiManutencaoFilmes.Models.Filme> Filmes { get; set; }
    }
}
