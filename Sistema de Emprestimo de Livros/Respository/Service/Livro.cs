using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Emprestimo_de_Livros.Data;
using Sistema_de_Emprestimo_de_Livros.Dto.Livro;
using Sistema_de_Emprestimo_de_Livros.Models;

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
            var nomeCaminhoImagem = GeraCaminhoImagem(foto);

            var livro = _mapper.Map<LivrosModel>(livroCriacaoDto);
            livro.Capa = nomeCaminhoImagem;

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


        public async Task<LivrosModel> BuscarLivroID(int? id)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);

            return livro;
        }

        public async Task<LivrosModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == livroEdicaoDto.Id);
            var nomeCaminhoImagem = " ";

            if (foto != null)
            {
                string caminhoCapaExistente = _caminhoServidor + "\\img\\" + livro.Capa;

                if (File.Exists(caminhoCapaExistente))
                {
                    File.Delete(caminhoCapaExistente);
                }

                nomeCaminhoImagem = GeraCaminhoImagem(foto);
            }

            if (livro != null)
            {
                _context.Entry(livro).State = EntityState.Detached;
            }

            var livroModel = _mapper.Map<LivrosModel>(livroEdicaoDto);

            if (nomeCaminhoImagem != " ")
            {
                livroModel.Capa = nomeCaminhoImagem;
            }
            else
            {
                livroModel.Capa = livro.Capa;
            }

            livroModel.DataAlteracao = DateTime.Now;

            _context.Update(livroModel);
            await _context.SaveChangesAsync();

            return livroModel;

        }


        public string GeraCaminhoImagem(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminho = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            string folderImg = _caminhoServidor + "\\img\\";

            //var arquivo = nomeCaminho;
            //var caminhoRelativo = "/img/" + arquivo;

            // condição para verificar  se há alguma pasta criada para salvar aquivos de imagem.
            if (!Directory.Exists(folderImg))
            {
                Directory.CreateDirectory(folderImg);
            }

            using (var stream = File.Create(folderImg + nomeCaminho))
            {

                foto.CopyToAsync(stream).Wait();
            }

            return nomeCaminho;
        }
    }
}
