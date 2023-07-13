using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            /*
             * SMTP -> Servidor que vai enviar a mensagem.
             */
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("miguelservemail@gmail.com", "kworoeyoeahjmqso");
            smtp.EnableSsl = true;

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
            mensagem.From = new MailAddress("miguelservemail@gmail.com");
            mensagem.To.Add(new MailAddress("miguelservemail@gmail.com"));
            mensagem.Subject = "Contato - LojaVirtual - E-mail: " + contato.Email; //assunto
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true; //para aceitar html no copro da msg

            smtp.Send(mensagem);//ENVIA VIA SMTP
        }
    }
}
