using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using SistemaVendas.Uteis;

namespace SistemaVendas.Models
{
    public class VendaModel
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string Cliente_Id { get; set; }
        public string Vendedor_Id { get; set; }
        public double Total { get; set; }
        public string ListaProdutos { get; set; }

        public List<VendaModel> ListagemVendas()
        {
            List<VendaModel> lista = new List<VendaModel>();
            VendaModel item;
            //TODO: Corrigir campo data
            //string sql = "SELECT v1.id, v1.data, v1.total, v2.nome as vendedor, c.nome as cliente FROM " +
            //             "venda v1 inner join vendedor v2 on v1.vendedor_id = v2.id inner join cliente c " +
            //             "on v1.cliente_id = c.id ORDER BY data, total";

            string sql = "SELECT v1.id,  v1.total, v2.nome as vendedor , c.nome as cliente " +
                         "FROM sistema_venda.venda v1 inner join sistema_venda.vendedor v2 on v1.vendedor_id = v2.id " +
                         "inner join sistema_venda.cliente c on v1.cliente_id = c.id ORDER BY total";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendaModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    //Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy"),
                    Total = double.Parse(dt.Rows[i]["total"].ToString()),
                    Cliente_Id = dt.Rows[i]["cliente"].ToString(),
                    Vendedor_Id = dt.Rows[i]["vendedor"].ToString()
                };
                lista.Add(item);
            }

            return lista;
        }

        public List<ClienteModel> RetornarListaClientes()
        {
            return new ClienteModel().ListarTodosClientes();
        }

        public List<VendedorModel> RetornarListaVendedores()
        {
            return new VendedorModel().ListarTodosVendedores();
        }

        public List<ProdutoModel> RetornarListaProdutos()
        {
            return new ProdutoModel().ListarTodosProdutos();
        }

        public void Inserir()
        {
            DAL objDAL = new DAL();

            //TODO: Corrigir campo data
            string dataVenda = DateTime.Now.Date.ToString("yyyy/MM/dd");
            //DateTime dataVenda = DateTime.Now;

            string sql = $"INSERT INTO venda (data, total, vendedor_id, cliente_id)" +
                $"VALUES({dataVenda}, {Total.ToString().Replace(",", ".")}, {Vendedor_Id}, {Cliente_Id})";
            objDAL.ExecutarComandoSQL(sql);


            //Recuperar o ID da venda
            sql = $"select id from venda where data={dataVenda} and vendedor_id={Vendedor_Id} and cliente_id={Cliente_Id} order by id desc limit 1";
            DataTable dt = objDAL.RetDataTable(sql);
            string id_venda = dt.Rows[0]["id"].ToString();

            //Serializar o JSON da lista de produtos selecionados e gravar na tabela itens_venda
            List<ItemVendaModel> lista_produtos = JsonConvert.DeserializeObject<List<ItemVendaModel>>(ListaProdutos);
            for (var i = 0; i < lista_produtos.Count; i++)
            {
                sql = "INSERT INTO itens_venda(venda_id, produto_id, qtde_produto, preco_produto) " +
                    $"VALUES ({id_venda},{lista_produtos[i].CodigoProduto.ToString()}," +
                    $"{lista_produtos[i].QtdeProduto.ToString()}," +
                    $"{lista_produtos[i].PrecoUnitario.ToString().Replace(",", ".")})";

                objDAL.ExecutarComandoSQL(sql);
            }

        }
    }
}
