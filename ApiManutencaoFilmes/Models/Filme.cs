using ApiManutencaoFilmes.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiManutencaoFilmes.Models {
    [Table("Filmes")]
    public class Filme {

        [Key]
        public int FilmeId { get; set; }

        [Required(ErrorMessage ="O campo Título é obrigatório!")]
        [StringLength(80, ErrorMessage = "O campo deve ter no máximo {1} caracteres!")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Diretor é obrigatório!")]
        [StringLength(80, ErrorMessage = "O campo deve ter no máximo {1} caracteres!")]
        public string Diretor { get; set; }

        public Genero Genero { get; set; }

        [Required(ErrorMessage = "O campo Gênero é obrigatório!")]
        public int GeneroId { get; set; }

        [StringLength(300, ErrorMessage ="O campo deve ter no máximo {1} caracteres!")]
        public string Sinopse { get; set; }
        
        [StringLength(4)]
        [ApenasNumeros]
        public string Ano { get; set; }
    }
}
