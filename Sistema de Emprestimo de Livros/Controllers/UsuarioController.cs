using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Sistema_de_Emprestimo_de_Livros.Enums;
using Sistema_de_Emprestimo_de_Livros.Logs_System;
using Sistema_de_Emprestimo_de_Livros.Respository.Interface;


namespace Sistema_de_Emprestimo_de_Livros.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuario;
        private readonly ILogs _logs;

        public UsuarioController(IUsuario usuario, ILogs logs)
        {
            _usuario = usuario;
            _logs = logs;
        }

        public async Task<IActionResult> Index(int? id)
        {            
            try
            {
                var usuarios = await _usuario.BuscarUsuarios(id);
                return View(usuarios);
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

		public IActionResult Cadastro(int? id)
		{
            ViewBag.Perfil = PerfilEnum.Administrador;

            if (id != null)
            {
				ViewBag.Perfil = PerfilEnum.Cliente;
			}

			return View();
		}
	}
}
