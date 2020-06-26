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
                         "venda v1 inner join vendedor v2 on v1.vendedor_id = v2.id inner join cliente c " +
                         "on v1.cliente_id = c.id " +
                         $"WHERE v1.data >= '{DataDe}' and v1.data <= '{DataAte}'" +
                         "ORDER BY data, total";

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

            string dataVenda = DateTime.Now.Date.ToString("yyyy-MM-dd");

            string sql = $"INSERT INTO venda (data, total, vendedor_id, cliente_id)" +
                $"VALUES('{dataVenda}', {Total.ToString().Replace(",", ".")}, {Vendedor_Id}, {Cliente_Id})";
            objDAL.ExecutarComandoSQL(sql);

            //Recuperar o ID da venda
            sql = $"select id from venda where data='{dataVenda}' and vendedor_id={Vendedor_Id} and cliente_id={Cliente_Id} order by id desc limit 1";
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

                //Baixar produto do estoque
                sql = "UPDATE produto " +
                    "SET quantidade_estoque = (quantidade_estoque - " + int.Parse(lista_produtos[i].QtdeProduto.ToString()) + ") " +
                    $"WHERE id = {lista_produtos[i].CodigoProduto.ToString()}";
                objDAL.ExecutarComandoSQL(sql);
            }

            objDAL.FecharConexao();
        }
    }
}
