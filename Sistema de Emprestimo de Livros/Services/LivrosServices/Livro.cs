using Microsoft.EntityFrameworkCore;
using Sistema_de_Emprestimo_de_Livros.Data;
using Sistema_de_Emprestimo_de_Livros.Dto.Livro;
using Sistema_de_Emprestimo_de_Livros.Models;

namespace Sistema_de_Emprestimo_de_Livros.Services.LivrosServices
{
    public class Livro : ILivro
    {
        private readonly AppDbContext _context;
        public Livro(AppDbContext context) 
        {
            _context = context;
        }  
        public async Task<List<LivrosModel>> BuscarLivros()
        {
            var livros = await _context.Livros.ToListAsync();
            return livros;
        }

		public Task<LivrosModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
		{
			
		}

		public bool VerificarCadastro(LivroCriacaoDto livroCriacao)
		{
			var livroBD = _context.Livros.FirstOrDefault(livro  => livro.ISBN == livroCriacao.ISBN);

            if (livroBD != null) 
            {
                return false;
            }

            return true;

		}
	}
}
