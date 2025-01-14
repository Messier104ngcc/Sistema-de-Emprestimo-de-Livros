using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Emprestimo_de_Livros.Dto.Livro
{
    public class LivroCriacaoDto
    {
        [Required]
        public string Capa { get; set; } = string.Empty;

        [Required]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Genero { get; set; } = string.Empty;

        [Required]
        public string Autor { get; set; } = string.Empty;

        [Required]
        public int AnoPublicacao { get; set; }

        [Required]
        public int QuantidadeEstoque { get; set; }

        [Required]
        public string Descricao { get; set; } = string.Empty;
    }
}
