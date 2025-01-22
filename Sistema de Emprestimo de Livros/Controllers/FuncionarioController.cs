using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sistema_de_Emprestimo_de_Livros.Logs_System;
using Sistema_de_Emprestimo_de_Livros.Respository.Interface;

namespace Sistema_de_Emprestimo_de_Livros.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IUsuario _usuario;
        private readonly ILogs _logs;

        public FuncionarioController(IUsuario usuario, ILogs log)
        {
            _usuario = usuario;
            _logs = log;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var funcinarios = await _usuario.BuscarUsuarios(null);
                return View(funcinarios);
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
    }
}
