using System.Data;
using MySql.Data.MySqlClient;

namespace SistemaVendas.Uteis
{
    //Data Access Layer
    public class DAL
    {
        private static string Server = "localhost";
        private static string Database = "sistema_venda";
        private static string User = "root";
        private static string Password = "";
        private static string ConnectionString = $"Server={Server};Database={Database};Uid={User};Pwd={Password};Charset=utf8;";
        private static MySqlConnection Connection;

        public DAL()
        {
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();

        }

        //Espera um parâmetro do tipo string
        //contendo um comando SQL do tipo SELECT
        public DataTable RetDataTable(string sql)
        {
            DataTable data = new DataTable();
            MySqlCommand Command = new MySqlCommand(sql, Connection);
            MySqlDataAdapter da = new MySqlDataAdapter(Command);
            da.Fill(data);

            return data;
        }

        public DataTable RetDataTable(MySqlCommand Command)
        {
            DataTable data = new DataTable();
            Command.Connection = Connection;
            MySqlDataAdapter da = new MySqlDataAdapter(Command);
            da.Fill(data);

            return data;
        }

        //Espera um parâmetro do tipo string
        //contendo um comando SQL do tipo INSERT, UPDATE, DELETE
        public void ExecutarComandoSQL(string sql)
        {
            MySqlCommand Command = new MySqlCommand(sql, Connection);
            Command.ExecuteNonQuery();
        }

        public void FecharConexao()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }

        }

        public MySqlCommand IniciarComando()
        {
            MySqlCommand Command = Connection.CreateCommand();
            return Command;
        }

        public MySqlTransaction IniciarTransacao()
        {
            return Connection.BeginTransaction();
        }

    }
}
