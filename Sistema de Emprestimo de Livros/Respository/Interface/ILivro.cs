﻿using Sistema_de_Emprestimo_de_Livros.Dto.Livro;
using Sistema_de_Emprestimo_de_Livros.Models;

namespace Sistema_de_Emprestimo_de_Livros.Respository.Service
{
    public interface ILivro
    {
        Task<List<LivrosModel>> BuscarLivros();

        bool VerificarCadastro(LivroCriacaoDto livroCriacao);

        Task<LivrosModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);

        Task<LivrosModel> BuscarLivroID(int? id);

        Task<LivrosModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto);
    }
}
