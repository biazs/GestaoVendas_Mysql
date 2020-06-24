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
        [DataType(DataType.Currency, ErrorMessage = "Preço unitário inválido")]
        public decimal? Preco_Unitario { get; set; }

        [Required(ErrorMessage = "Informe a quantidade em estoque do produto.")]
        [Range(1, 9999, ErrorMessage = "Valor deve ser entre 1 e 999999999")]
        public decimal? Quantidade_Estoque { get; set; }

        [Required(ErrorMessage = "Informe a unidade de medida do produto.")]
        public string Unidade_Medida { get; set; }

        [Required(ErrorMessage = "Informe o link da imagem do produto.")]
        public string Link_Foto { get; set; }

        public string Fornecedor_Id { get; set; }


        public List<ProdutoModel> ListarTodosProdutos()
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();
            ProdutoModel item;
            string sql = "SELECT p.id, p.nome, p.descricao, p.preco_unitario, p.quantidade_estoque, p.unidade_medida, p.link_foto, " +
                         $"f.nome as nome_fornecedor " +
                         $"FROM produto p inner join fornecedor f on p.fornecedor_id = f.id " +
                         "ORDER BY nome asc";
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
                    Fornecedor_Id = dt.Rows[i]["nome_fornecedor"].ToString(),
                    Link_Foto = dt.Rows[i]["link_foto"].ToString(),
                };
                lista.Add(item);
            }

            return lista;
        }

        public ProdutoModel RetornarProduto(int? id)
        {
            ProdutoModel item;
            string sql = $"SELECT p.id, p.nome, p.descricao, p.preco_unitario, p.quantidade_estoque, p.unidade_medida, p.link_foto, " +
                         $"f.nome as nome_fornecedor " +
                         $"FROM produto p inner join fornecedor f on p.fornecedor_id = f.id " +
                         $"WHERE p.id = '{id}' ORDER BY p.nome asc";

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
                Fornecedor_Id = dt.Rows[0]["nome_fornecedor"].ToString(),
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
                    $"link_foto = '{Link_Foto}', " +
                    $"fornecedor_id = '{Fornecedor_Id}' " +
                    $"WHERE id = '{Id}'";
            }
            else
            {
                sql = $"INSERT into produto(nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto, fornecedor_id ) " +
                      $"VALUES ('{Nome}','{Descricao}', '{Preco_Unitario}','{Quantidade_Estoque}', '{Unidade_Medida}','{Link_Foto}', '{Fornecedor_Id}')";
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
