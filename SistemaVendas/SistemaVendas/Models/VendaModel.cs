using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
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


        //Para atender filtro do relatório
        public List<VendaModel> ListagemVendas(string DataDe, string DataAte)
        {
            return RetornarListagemVendas(DataDe, DataAte);
        }

        //Listagem Geral
        public List<VendaModel> ListagemVendas()
        {
            return RetornarListagemVendas("1900-01-01", "2300-01-01");
        }

        private List<VendaModel> RetornarListagemVendas(string DataDe, string DataAte)
        {
            List<VendaModel> lista = new List<VendaModel>();
            VendaModel item;

            string sql = "SELECT v1.id, v1.data, v1.total, v2.nome as vendedor, c.nome as cliente FROM " +
                         "sistema_venda.venda v1 inner join sistema_venda.vendedor v2 on v1.vendedor_id = v2.id inner join sistema_venda.cliente c " +
                         "on v1.cliente_id = c.id " +
                         $"WHERE v1.data >= '{DataDe}' and v1.data <= '{DataAte}'" +
                         "ORDER BY data desc, id desc, total";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendaModel
                {
                    Id = dt.Rows[i]["id"].ToString(),
                    Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd-MM-yyyy"),
                    Total = double.Parse(dt.Rows[i]["total"].ToString()),
                    Cliente_Id = dt.Rows[i]["cliente"].ToString(),
                    Vendedor_Id = dt.Rows[i]["vendedor"].ToString()
                };
                lista.Add(item);
            }

            objDAL.FecharConexao();

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

            MySqlCommand myCommand = objDAL.IniciarComando();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = objDAL.IniciarTransacao();

            string dataVenda = DateTime.Now.Date.ToString("yyyy-MM-dd");

            myCommand.Transaction = myTrans;

            string sql = $"INSERT INTO sistema_venda.venda (data, total, vendedor_id, cliente_id)" +
            $"VALUES('{dataVenda}', {Total.ToString().Replace(",", ".")}, {Vendedor_Id}, {Cliente_Id})";
            objDAL.ExecutarComandoSQL(sql);

            //Recuperar o ID da venda
            sql = $"select id from sistema_venda.venda where data='{dataVenda}' and vendedor_id={Vendedor_Id} and cliente_id={Cliente_Id} order by id desc limit 1";
            DataTable dt = objDAL.RetDataTable(sql);
            string id_venda = dt.Rows[0]["id"].ToString();

            //Serializar o JSON da lista de produtos selecionados e gravar na tabela itens_venda
            List<ItemVendaModel> lista_produtos = JsonConvert.DeserializeObject<List<ItemVendaModel>>(ListaProdutos);
            for (var i = 0; i < lista_produtos.Count; i++)
            {
                sql = "INSERT INTO sistema_venda.itens_venda(venda_id, produto_id, qtde_produto, preco_produto) " +
                    $"VALUES ({id_venda},{lista_produtos[i].CodigoProduto.ToString()}," +
                    $"{lista_produtos[i].QtdeProduto.ToString()}," +
                    $"{lista_produtos[i].PrecoUnitario.ToString().Replace(",", ".")})";

                objDAL.ExecutarComandoSQL(sql);


                //Buscar o ID do estoque
                sql = $"SELECT estoque_id FROM sistema_venda.produto_estoque WHERE produto_id = '{lista_produtos[i].CodigoProduto.ToString()}'";
                dt = objDAL.RetDataTable(sql);
                string id_estoque = dt.Rows[0]["estoque_id"].ToString();

                //Baixar quantidade do estoque
                sql = "UPDATE sistema_venda.estoque " +
                    "SET quantidade = (quantidade - " + int.Parse(lista_produtos[i].QtdeProduto.ToString()) + ") " +
                    $"WHERE id = {id_estoque}";
                objDAL.ExecutarComandoSQL(sql);
            }

            myTrans.Commit();

            objDAL.FecharConexao();

        }
    }
}
