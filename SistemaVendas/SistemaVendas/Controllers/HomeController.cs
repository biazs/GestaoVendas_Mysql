using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Menu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                bool loginOk = login.ValidarLogin();
                if (loginOk)
                {
                    HttpContext.Session.SetString("IdUsuarioLogado", login.Id);
                    HttpContext.Session.SetString("NomeUsuarioLogado", login.Nome);

                    return RedirectToAction("Menu", "Home");
                }
                else
                {
                    TempData["ErrorLogin"] = "E-mail ou Senha inválidos.";
                }
            }
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
