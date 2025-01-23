using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sistema_de_Emprestimo_de_Livros.Models
{
    public class Enderecos
    {
        [Key]
        public int Id { get; set; }    

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Estado { get; set; }

        public string? Complemento { get; set; }

        public int UsuarioId { get; set; }

        [JsonIgnore]
        public UsuariosModel Usuario { get; set; }
    }
}
