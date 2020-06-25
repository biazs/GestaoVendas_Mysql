using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using SistemaVendas.Models;

namespace SistemaVendas.Libraries.Email
{
    public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;
        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }
        public void EnviarContatoPorEmail(ClienteModel cliente)
        {

            string corpoMsg = string.Format("<h2>Contato - Sistema de Vendas</h2" +
                "<b>Nome</b> {0} <br />" +
                "<b>E-mail</b> {1} <br />" +
                "<b>Texto</b> preencher texto <br />" +
                "<br />Email enviado automaticamente do site - Sistema de Vendas.",
                cliente.Nome, cliente.Email
                );

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email: UserName"));
            mensagem.To.Add("email@gmail.com");
            mensagem.Subject = "Contato - Sistema de Vendas";
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            _smtp.Send(mensagem);
        }
    }
}
