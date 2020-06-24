using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using SistemaVendas.Uteis;

namespace SistemaVendas.Models
{
    public class FornecedorModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Informe o CNPJ do fornecedor.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Informe o nome do fornecedor.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o nome do contado do fornecedor.")]
        public string Contato { get; set; }

        [Required(ErrorMessage = "Informe o elefone em estoque do fornecedor.")]
        public string Telefone { get; set; }

        public List<FornecedorModel> ListarTodosFornecedores()
        {

            List<FornecedorModel> lista = new List<FornecedorModel>();
            FornecedorModel item;
            string sql = $"select id, nome, cnpj, contato, telefone FROM fornecedor ORDER BY nome asc";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new FornecedorModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    CNPJ = dt.Rows[i]["cnpj"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Contato = dt.Rows[i]["contato"].ToString(),
                    Telefone = dt.Rows[i]["telefone"].ToString()
                };
                lista.Add(item);
            }

            return lista;
        }


        public FornecedorModel RetornarFornecedor(int? id)
        {
            FornecedorModel item;
            string sql = $"SELECT id, nome, cnpj, contato, telefone FROM fornecedor WHERE id = '{id}' ORDER BY nome asc";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item = new FornecedorModel
            {
                Id = dt.Rows[0]["id"].ToString(),
                CNPJ = dt.Rows[0]["cnpj"].ToString(),
                Nome = dt.Rows[0]["nome"].ToString(),
                Contato = dt.Rows[0]["contato"].ToString(),
                Telefone = dt.Rows[0]["telefone"].ToString()
            };

            return item;
        }

        public void Gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;

            if (Id != null)
            {
                sql = $"UPDATE fornecedor SET " +
                    $"nome = '{Nome}', " +
                    $"contato = '{Contato}', " +
                    $"cnpj = '{CNPJ}', " +
                    $"telefone = '{Telefone}' " +
                    $"WHERE id = '{Id}'";
            }
            else
            {
                sql = $"INSERT into fornecedor(nome, cnpj, contato, telefone) " +
                      $"VALUES ('{Nome}','{CNPJ}', '{Contato}','{Telefone}')";
            }

            objDAL.ExecutarComandoSQL(sql);

        }

        public void Excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM fornecedor WHERE id = '{id}'";
            objDAL.ExecutarComandoSQL(sql);
        }

        public List<FornecedorModel> RetornarListaFornecedores()
        {
            return new FornecedorModel().ListarTodosFornecedores();
        }
    }


}
