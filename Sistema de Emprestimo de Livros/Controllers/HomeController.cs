using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Sistema_de_Emprestimo_de_Livros.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
