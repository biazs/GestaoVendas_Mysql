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

        [Required(ErrorMessage = "Informe o preço unitário do produto.")]
        public decimal Preco_Unitario { get; set; }

        [Required(ErrorMessage = "Informe a quantidade em estoque do produto.")]
        public decimal Quantidade_Estoque { get; set; }

        [Required(ErrorMessage = "Informe a unidade de medida do produto.")]
        public string Unidade_Medida { get; set; }

        [Required(ErrorMessage = "Informe o link da imagem do produto.")]
        public string Link_Foto { get; set; }


        public List<ProdutoModel> ListarTodosProdutos()
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();
            ProdutoModel item;
            string sql = $"select id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM produto ORDER BY nome asc";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ProdutoModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Descricao = dt.Rows[i]["descricao"].ToString(),
                    Preco_Unitario = decimal.Parse(dt.Rows[i]["preco_unitario"].ToString()),
                    Quantidade_Estoque = decimal.Parse(dt.Rows[i]["quantidade_estoque"].ToString()),
                    Unidade_Medida = dt.Rows[i]["unidade_medida"].ToString(),
                    Link_Foto = dt.Rows[i]["link_foto"].ToString(),
                };
                lista.Add(item);
            }

            return lista;
        }

        public ProdutoModel RetornarProduto(int? id)
        {
            ProdutoModel item;
            string sql = $"select id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM produto WHERE id = '{id}' ORDER BY nome asc";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item = new ProdutoModel
            {
                Id = dt.Rows[0]["id"].ToString(),
                Nome = dt.Rows[0]["nome"].ToString(),
                Descricao = dt.Rows[0]["descricao"].ToString(),
                Preco_Unitario = decimal.Parse(dt.Rows[0]["preco_unitario"].ToString()),
                Quantidade_Estoque = decimal.Parse(dt.Rows[0]["quantidade_estoque"].ToString()),
                Unidade_Medida = dt.Rows[0]["unidade_medida"].ToString(),
                Link_Foto = dt.Rows[0]["link_foto"].ToString(),
            };

            return item;
        }

        public void Gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;

            if (Id != null)
            {
                sql = $"UPDATE produto SET " +
                    $"nome = '{Nome}', " +
                    $"descricao = '{Descricao}', " +
                    $"preco_unitario = '{Preco_Unitario.ToString().Replace(",", ".")}', " +
                    $"quantidade_estoque = '{Quantidade_Estoque}', " +
                    $"unidade_medida = '{Unidade_Medida}', " +
                    $"link_foto = '{Link_Foto}' " +
                    $"WHERE id = '{Id}'";
            }
            else
            {
                sql = $"INSERT into produto(nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto ) " +
                      $"VALUES ('{Nome}','{Descricao}', '{Preco_Unitario}','{Quantidade_Estoque}', '{Unidade_Medida}','{Link_Foto}')";
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
