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
        public VendaController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            CarregaLista();
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VendaModel venda)
        {
            //captura o id do usuário logado no sistema
            venda.Vendedor_Id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            venda.Inserir();
            CarregarDados();
            TempData["MSG_S"] = Mensagem.MSG_S001;
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
