using System.Collections.Generic;
using SistemaVendas.Models;
using Xunit;

namespace SistemaVendasTestes
{
    public class UnitTestModels
    {
        [Fact]
        public void TestLogin_Ok()
        {
            LoginModel objLogin = new LoginModel();
            objLogin.Email = "fabianazs@gmail.com";
            objLogin.Senha = "123456";

            bool result = objLogin.ValidarLogin();
            Assert.True(result);
        }

        [Fact]
        public void TestLogin_False()
        {
            LoginModel objLogin = new LoginModel();
            objLogin.Email = "fabianazs@gmail.com";
            objLogin.Senha = "12345";

            bool result = objLogin.ValidarLogin();
            Assert.False(result);

        }

        [Fact]
        public void CheckTypeListProdutos()
        {
            ProdutoModel objProduto = new ProdutoModel();

            var lista = objProduto.ListarTodosProdutos();
            Assert.IsType<List<ProdutoModel>>(lista);

        }
    }
}
