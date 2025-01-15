using Microsoft.EntityFrameworkCore;
using Sistema_de_Emprestimo_de_Livros.Data;
using Sistema_de_Emprestimo_de_Livros.Dto.Livro;
using Sistema_de_Emprestimo_de_Livros.Models;

namespace Sistema_de_Emprestimo_de_Livros.Services.LivrosServices
{
    public class Livro : ILivro
    {
        private readonly AppDbContext _context;
        private string _caminhoServidor;

        public Livro(AppDbContext context, IWebHostEnvironment sistema) 
        {
            _context = context;
            _caminhoServidor = sistema.WebRootPath;
        }  
        public async Task<List<LivrosModel>> BuscarLivros()
        {
            var livros = await _context.Livros.ToListAsync();
            return livros;
        }

		public async Task<LivrosModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
		{
			var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminho = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + livroCriacaoDto.ISBN + ".png";

            string folderImg = _caminhoServidor + "\\img\\";

            // condição para verificar  se há alguma pasta criada para salvar aquivos de imagem.
            if (!Directory.Exists(folderImg)) 
            {
                Directory.CreateDirectory(folderImg);
            }

            using (var stream = System.IO. File.Create(folderImg + nomeCaminho))
            {
                
                foto.CopyToAsync(stream).Wait();
            }

            var livro = new LivrosModel
		{
				Titulo = livroCriacaoDto.Titulo,
				Capa = folderImg,
				Autor = livroCriacaoDto.Autor,
				Descricao = livroCriacaoDto.Descricao,
				QuantidadeEstoque = livroCriacaoDto.QuantidadeEstoque,
				AnoPublicacao = livroCriacaoDto.AnoPublicacao,
				ISBN = livroCriacaoDto.ISBN,
				Genero = livroCriacaoDto.Genero,
			};

            _context.Add(livro);
            await _context.SaveChangesAsync();
			
            return livro;
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
