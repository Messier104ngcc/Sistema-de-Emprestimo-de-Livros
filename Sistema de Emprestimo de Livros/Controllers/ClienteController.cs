using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sistema_de_Emprestimo_de_Livros.Logs_System;
using Sistema_de_Emprestimo_de_Livros.Respository.Interface;

namespace Sistema_de_Emprestimo_de_Livros.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IUsuario _usuario;
        private readonly ILogs _logs;

        public ClienteController(IUsuario usuario, ILogs log)
        {
            _usuario = usuario;
            _logs = log;
        }

        public async Task<IActionResult> Index(int? id)
        {
            try
            {
                var cliente = await _usuario.BuscarUsuarios(id);
                return View(cliente);
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
