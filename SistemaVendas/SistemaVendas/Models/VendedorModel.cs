using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using SistemaVendas.Uteis;

namespace SistemaVendas.Models
{
    public class VendedorModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Informe o nome do vendedor.")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Informe o e-mail do vendedor.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
        public string Email { get; set; }

        public string Senha { get; set; }

        public List<VendedorModel> ListarTodosVendedores()
        {
            List<VendedorModel> lista = new List<VendedorModel>();
            VendedorModel item;
            string sql = $"select id, nome, email, senha FROM vendedor ORDER BY nome asc";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendedorModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Email = dt.Rows[i]["email"].ToString(),
                    Senha = dt.Rows[i]["senha"].ToString(),
                };
                lista.Add(item);
            }

            return lista;
        }

        public VendedorModel RetornarVendedor(int? id)
        {
            VendedorModel item;
            string sql = $"select id, nome, email, senha FROM vendedor WHERE id = '{id}' ORDER BY nome asc";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item = new VendedorModel
            {
                Id = dt.Rows[0]["id"].ToString(),
                Nome = dt.Rows[0]["nome"].ToString(),
                Email = dt.Rows[0]["email"].ToString(),
                Senha = dt.Rows[0]["senha"].ToString(),
            };

            return item;
        }

        public void Gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;

            if (Id != null)
            {
                sql = $"UPDATE vendedor SET nome = '{Nome}',  email = '{Email}' WHERE id = '{Id}'";
            }
            else
            {
                sql = $"INSERT into vendedor(nome, email, senha) VALUES ('{Nome}', '{Email}', '123456')";
            }

            objDAL.ExecutarComandoSQL(sql);

        }

        public void Excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM vendedor WHERE id = '{id}'";
            objDAL.ExecutarComandoSQL(sql);
        }
    }
}
