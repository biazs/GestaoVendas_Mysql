using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using SistemaVendas.Uteis;

namespace SistemaVendas.Models
{
    public class ProdutoModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do produto.")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Informe a descrição do produto.")]
        public string Descricao { get; set; }


        public List<ProdutoModel> ListarTodosProdutos()
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();
            ProdutoModel item;
            string sql = $"select id, nome, descricao FROM produto ORDER BY nome asc";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ProdutoModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Descricao = dt.Rows[i]["descricao"].ToString(),
                };
                lista.Add(item);
            }

            return lista;
        }

        public ProdutoModel RetornarProduto(int? id)
        {
            ProdutoModel item;
            string sql = $"select id, nome, descricao FROM produto WHERE id = '{id}' ORDER BY nome asc";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item = new ProdutoModel
            {
                Id = dt.Rows[0]["id"].ToString(),
                Nome = dt.Rows[0]["nome"].ToString(),
                Descricao = dt.Rows[0]["descricao"].ToString(),
            };

            return item;
        }

        public void Gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;

            if (Id != null)
            {
                sql = $"UPDATE produto SET nome = '{Nome}', descricao = '{Descricao}' WHERE id = '{Id}'";
            }
            else
            {
                sql = $"INSERT into produto(nome, descricao) VALUES ('{Nome}','{Descricao}')";
            }

            objDAL.ExecutarComandoSQL(sql);

        }

        public void Excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM produto WHERE id = '{id}'";
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
