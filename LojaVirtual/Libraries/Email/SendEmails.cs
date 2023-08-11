using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class SendEmails
    {
        private SmtpClient _smtp;
        private IConfiguration _configuration;
        public SendEmails(SmtpClient smtp,IConfiguration configuration)
        {
            _configuration = configuration;
            _smtp = smtp;
        }
        public void EnviarContatoPorEmail(Contato contato)
        {
           
            string corpoMsg = string.Format("<h2>Conato - Loja Virtual </h2>"
                + "<b>Nome: </b>{0} <br />"
                + "<b>E-mail: </b>{1} <br />"
                + "<b>Texto: </b> {2} <br />"
                + "<br /> E-mail enviado automaticamente do site LojaVirtual.", contato.Nome, contato.Email, contato.Texto
                );
            /*
             * MailMessage -> Construir a mensagem
             */
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(new MailAddress("miguelservemail@gmail.com"));
            mensagem.Subject = "Contato - LojaVirtual - E-mail: " + contato.Email; //assunto
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true; //para aceitar html no copro da msg

            _smtp.Send(mensagem);//ENVIA VIA SMTP
        }
    }
}
