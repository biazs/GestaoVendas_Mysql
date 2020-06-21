using System.Data;
using SistemaVendas.Uteis;

namespace SistemaVendas.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public int Senha { get; set; }

        public ValidarLogin()
        {
            string sql = $"SELECT ID FROM VENDEDOR WHERE EMAIL={Email}' AND SENHA={Senha}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);
        }
    }
}
