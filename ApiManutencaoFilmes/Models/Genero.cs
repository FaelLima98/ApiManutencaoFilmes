using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiManutencaoFilmes.Models {
    [Table("Generos")]
    public class Genero {

        public Genero() {
            Filmes = new Collection<Filme>();
        }

        [Key]
        public int GeneroId { get; set; }

        public string Nome { get; set; }

        public ICollection<Filme> Filmes { get; set; }
    }
}
