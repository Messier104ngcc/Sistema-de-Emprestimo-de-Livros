using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Emprestimo_de_Livros.Data;
using Sistema_de_Emprestimo_de_Livros.Dto.Livro;
using Sistema_de_Emprestimo_de_Livros.Models;
using Sistema_de_Emprestimo_de_Livros.Services.LivrosServices;

namespace Sistema_de_Emprestimo_de_Livros.Respository.Service
{
    public class Livro : ILivro
    {
        private readonly AppDbContext _context;
        private string _caminhoServidor;
        private readonly IMapper _mapper;

        public Livro(AppDbContext context, IWebHostEnvironment sistema, IMapper mapper)
        {
            _context = context;
            _caminhoServidor = sistema.WebRootPath;
            _mapper = mapper;
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

            var arquivo = nomeCaminho;
            var caminhoRelativo = "/img/" + arquivo;

            // condição para verificar  se há alguma pasta criada para salvar aquivos de imagem.
            if (!Directory.Exists(folderImg))
            {
                Directory.CreateDirectory(folderImg);
            }

            using (var stream = File.Create(folderImg + nomeCaminho))
            {

                foto.CopyToAsync(stream).Wait();
            }

            var livro = _mapper.Map<LivrosModel>(livroCriacaoDto);
            livro.Capa = caminhoRelativo;

            _context.Add(livro);
            await _context.SaveChangesAsync();

            return livro;
        }

        public bool VerificarCadastro(LivroCriacaoDto livroCriacao)
        {
            var livroBD = _context.Livros.FirstOrDefault(livro => livro.ISBN == livroCriacao.ISBN);

            if (livroBD != null)
            {
                return false;
            }

            return true;

        }
    }
}
