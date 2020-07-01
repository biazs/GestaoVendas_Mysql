using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using SistemaVendas.Libraries.Mensagem;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class VendaController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private VendedorModel _vendedor;
        public VendaController(IHttpContextAccessor httpContextAccessor, VendedorModel vendedor)
        {
            _httpContextAccessor = httpContextAccessor;
            _vendedor = vendedor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                CarregaLista();
                return View();
            }

            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao carregar registros. Tente novamente mais tarde. \n\n" + e.Message });
            }
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            CarregarDados();
            ViewBag.Vendedor = _vendedor.RetornarVendedor(Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado")));
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VendaModel venda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //captura o id do usuário logado no sistema
                    venda.Vendedor_Id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
                    ViewBag.Vendedor = _vendedor.RetornarVendedor(Convert.ToInt32(venda.Vendedor_Id));
                    venda.Inserir();
                    TempData["MSG_S"] = Mensagem.MSG_S001;
                }


                catch (Exception e)
                {
                    return RedirectToAction(nameof(Error), new { message = "Erro ao registrar venda. \n" + e.Message });
                }
            }

            CarregarDados();
            return View();
        }

        public IActionResult VisualizarComoPDF()
        {
            CarregaLista();

            var pdf = new ViewAsPdf
            {
                ViewName = "VisualizarComoPDF",
                IsGrayScale = true,
                Model = ViewBag.ListaVendas
            };

            return pdf;
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        private void CarregaLista()
        {
            ViewBag.ListaVendas = new VendaModel().ListagemVendas();
        }

        private void CarregarDados()
        {
            ViewBag.ListaClientes = new VendaModel().RetornarListaClientes();
            ViewBag.ListaVendedores = new VendaModel().RetornarListaVendedores();
            ViewBag.ListaProdutos = new VendaModel().RetornarListaProdutos();
        }
    }
}
