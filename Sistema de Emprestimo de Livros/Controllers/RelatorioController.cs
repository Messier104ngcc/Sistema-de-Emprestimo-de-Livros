using Microsoft.AspNetCore.Mvc;

namespace Sistema_de_Emprestimo_de_Livros.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
