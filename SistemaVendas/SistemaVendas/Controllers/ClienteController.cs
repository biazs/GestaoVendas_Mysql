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
            ViewBag.ListaClientes = new ClienteModel().ListarTodosClientes();
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
            ViewBag.ListaClientes = new ClienteModel().ListarTodosClientes();

            var pdf = new ViewAsPdf
            {
                ViewName = "VisualizarComoPDF",
                IsGrayScale = true,
                Model = ViewBag.ListaClientes
            };
            return pdf;

            /* var pdf = new ViewAsPdf(ViewBag.ListaClientes[0], null);
             for (int i = 1; i < ViewBag.ListaClientes.Count; i++)
             {
                 pdf = new ViewAsPdf
                 {
                     ViewName = "VisualizarComoPDF",
                     Model = new ClienteModel().ListarTodosClientes()[i],
                     ViewData = null
                 };
             }
             return pdf;*/

            /* var pdf = new ViewAsPdf(ViewBag.ListaClientes[0], null);

             //for (int i = 1; i < ViewBag.ListaClientes.Count; i++)
             //{
             //    pdf = new ViewAsPdf(ViewBag.ListaClientes[i], null);
             //}

             return pdf;
            */

            /* // com um registro só
            var pdf = new ViewAsPdf(ViewBag.ListaClientes[1]);

            return pdf;
            */

            //var pdf = new ViewAsPdf();

            //for (int i = 0; i < ViewBag.ListaClientes.Count; i++)
            //{
            //pdf = new ViewAsPdf
            //{
            //    ViewName = "VisualizarComoPDF",
            //    Model = new ClienteModel().ListarTodosClientes()[i],
            //    ViewData = null
            //};
            //}


        }


    }
}
