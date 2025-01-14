using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Emprestimo_de_Livros.Models
{
    public class LivrosModel
    {
        [Key]
        public int Id { get; set; }

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
		[DataType(DataType.Date)]
		public DateTime AnoPublicacao { get; set; }

        [Required]
        public int QuantidadeEstoque { get; set; }
        //public ICollection<EmprestimoModel> Emprestimos { get; set; }

        [Required]
        public string Descricao { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}
