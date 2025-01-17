using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sistema_de_Emprestimo_de_Livros.Dto.Livro;
using Sistema_de_Emprestimo_de_Livros.Logs_System;
using Sistema_de_Emprestimo_de_Livros.Models;
using Sistema_de_Emprestimo_de_Livros.Services.LivrosServices;

namespace Sistema_de_Emprestimo_de_Livros.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ILivro _livro;
        private readonly ILogs _logs;

        public LivrosController(ILivro livro, ILogs logs) 
        {
            _livro = livro;
            _logs = logs;
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
    }
}
