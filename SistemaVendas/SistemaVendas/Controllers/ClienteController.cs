using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using SistemaVendas.Libraries.Mensagem;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            CarregaLista();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if (id != null)
            {
                ViewBag.Cliente = new ClienteModel().RetornarCliente(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Gravar();
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Excluir(int id)
        {
            ViewData["idExcluir"] = id;
            return View();
        }

        public IActionResult ExcluirCliente(int id)
        {
            new ClienteModel().Excluir(id);
            return View();
        }

        public IActionResult VisualizarComoPDF()
        {
            CarregaLista();

            var pdf = new ViewAsPdf
            {
                ViewName = "VisualizarComoPDF",
                IsGrayScale = true,
                Model = ViewBag.ListaClientes
            };

            return pdf;
        }
        private void CarregaLista()
        {
            ViewBag.ListaClientes = new ClienteModel().ListarTodosClientes();
        }

    }
}
