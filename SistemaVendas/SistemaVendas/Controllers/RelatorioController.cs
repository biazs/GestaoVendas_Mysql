using Microsoft.AspNetCore.Mvc;

namespace SistemaVendas.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Vendas()
        {
            return View();
        }
        public IActionResult Grafico()
        {
            return View();
        }
        public IActionResult Comissao()
        {
            return View();
        }
    }
}
