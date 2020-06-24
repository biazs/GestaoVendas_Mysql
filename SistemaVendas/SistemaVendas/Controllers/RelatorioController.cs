using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Vendas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Vendas(RelatorioModel relatorio)
        {
            if (relatorio.DataDe.Year == 1)
            {
                ViewBag.ListaVendas = new VendaModel().ListagemVendas();
            }
            else
            {
                string DataDe = relatorio.DataDe.ToString("yyyy/MM/dd");
                string DataAte = relatorio.DataAte.ToString("yyyy/MM/dd");
                ViewBag.ListaVendas = new VendaModel().ListagemVendas(DataDe, DataAte);
            }

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
