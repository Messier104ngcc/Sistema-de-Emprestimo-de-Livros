using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sistema_de_Emprestimo_de_Livros.Dto.Livro;
using Sistema_de_Emprestimo_de_Livros.Logs_System;
using Sistema_de_Emprestimo_de_Livros.Models;
using Sistema_de_Emprestimo_de_Livros.Respository.Service;
using Sistema_de_Emprestimo_de_Livros.Services.LivrosServices;

namespace Sistema_de_Emprestimo_de_Livros.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ILivro _livro;
        private readonly ILogs _logs;
        private readonly IMapper _mapper;

        public LivrosController(ILivro livro, ILogs logs, IMapper mapper) 
        {
            _livro = livro;
            _logs = logs;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<LivrosModel>>> Index()
        {
            try
            {
                var livro = await _livro.BuscarLivros();
                return View(livro);
            }
            catch (SqlException ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro ao conectar ao banco de dados. Verifique a conexão e tente novamente.";
                return View("Index");
            }
            catch (Exception ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["WarningMessage"] = "Erro inesperado. Contate o suporte.";
                return View("Index");
            }
        }
     
        public IActionResult Cadastro()
        {
            return View();
        }

        public async Task<ActionResult> Detalhes(int id) 
        {           
			try
			{
				if (id != null)
				{
					var livros = await _livro.BuscarLivroID(id);
					return View(livros);
				}

			}
			catch (SqlException ex)
			{
				_logs.RegistrarLogDeExcecao(ex);
				TempData["MensagemInfo"] = "Erro ao conectar ao banco de dados. Verifique a conexão e tente novamente.";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				_logs.RegistrarLogDeExcecao(ex);
				TempData["MensagemErro"] = "Erro inesperado. Contate o suporte.";
				return RedirectToAction("Index");
			}			

            return RedirectToAction("Index");
		}

        public async Task<ActionResult> Editar(int id)
        {
            try
            {
                if (id != null)
                {
                    var livros = await _livro.BuscarLivroID(id);
                    var livroEdicaoDto = _mapper.Map<LivroEdicaoDto>(livros);
                    return View(livroEdicaoDto);
                }

            }
            catch (SqlException ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["MensagemInfo"] = "Erro ao conectar ao banco de dados. Verifique a conexão e tente novamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logs.RegistrarLogDeExcecao(ex);
                TempData["MensagemErro"] = "Erro inesperado. Contate o suporte.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(LivroCriacaoDto livrosDto, IFormFile foto)
        {
			try
			{
                if (foto != null)
                {
                    if (livrosDto != null)
                    {
                        if (ModelState.IsValid)
                        {
                            if (!_livro.VerificarCadastro(livrosDto))
                            {
                                TempData["MensagemErro"] = "Código ISBN já cadastrado!";
                                return View("Cadastro");
                            }

                            var livro = await _livro.Cadastrar(livrosDto, foto);

                            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                            return RedirectToAction("Index");
                        }
                        else
                        {							
							return View("Cadastro");
                        }
                    }
                    else
                    {
						return View("Cadastro");
                    }
                }
                else 
                {
					TempData["MensagemErro"] = "Inclua uma imagem de capa!";
					return View("Cadastro");
				}

			}
			catch (SqlException ex)
			{
				_logs.RegistrarLogDeExcecao(ex);
				TempData["MensagemInfo"] = "Erro ao conectar ao banco de dados. Verifique a conexão e tente novamente.";
				return View("Index");
			}
			catch (Exception ex)
			{
				_logs.RegistrarLogDeExcecao(ex);
				TempData["MensagemErro"] = "Erro inesperado. Contate o suporte.";
				return View("Index");
			}
		}

		[HttpPost]
		public async Task<ActionResult> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile? foto)
		{
			try
			{
				if (ModelState.IsValid)
				{					
					var livro = await _livro.Editar(livroEdicaoDto, foto);

					TempData["MensagemSucesso"] = "Edição realizado com sucesso!";
					return RedirectToAction("Index");
				}
				else
				{
					TempData["MensagemSucesso"] = "Verifique os campos preenchidos!";
					return View("Cadastro");
				}
			}
			catch (SqlException ex)
			{
				_logs.RegistrarLogDeExcecao(ex);
				TempData["MensagemInfo"] = "Erro ao conectar ao banco de dados. Verifique a conexão e tente novamente.";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				_logs.RegistrarLogDeExcecao(ex);
				TempData["MensagemErro"] = "Erro inesperado. Contate o suporte.";
				return RedirectToAction("Index");
			}
		}
	}
}
