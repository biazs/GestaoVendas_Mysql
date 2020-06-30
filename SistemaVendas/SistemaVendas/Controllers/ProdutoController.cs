using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using SistemaVendas.Libraries.Mensagem;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            CarregaLista();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            CarregarDados();
            if (id != null)
            {
                ViewBag.Produto = new ProdutoModel().RetornarProduto(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoModel produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    produto.Gravar();
                    TempData["MSG_S"] = Mensagem.MSG_S001;
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return RedirectToAction(nameof(Error), new { message = "Erro ao preencher produto. \n" + e.Message });
                }


            }
            return View();
        }

        public IActionResult Excluir(int id)
        {
            ViewData["idExcluir"] = id;
            return View();
        }

        public IActionResult ExcluirProduto(int id)
        {
            new ProdutoModel().Excluir(id);
            return View();
        }

        public IActionResult VisualizarComoPDF()
        {
            CarregaLista();

            var pdf = new ViewAsPdf
            {
                ViewName = "VisualizarComoPDF",
                IsGrayScale = true,
                Model = ViewBag.ListaProdutos
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
            ViewBag.ListaProdutos = new ProdutoModel().ListarTodosProdutos();
        }

        private void CarregarDados()
        {
            ViewBag.ListaFornecedores = new FornecedorModel().RetornarListaFornecedores();
        }
    }
}
