using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class FornecedorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaFornecedores = new FornecedorModel().ListarTodosFornecedores();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if (id != null)
            {
                ViewBag.Fornecedor = new FornecedorModel().RetornarFornecedor(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(FornecedorModel fornecedor)
        {
            if (ModelState.IsValid)
            {
                fornecedor.Gravar();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Excluir(int id)
        {
            ViewData["idExcluir"] = id;
            return View();
        }

        public IActionResult ExcluirFornecedor(int id)
        {
            new FornecedorModel().Excluir(id);
            return View();
        }

    }
}
