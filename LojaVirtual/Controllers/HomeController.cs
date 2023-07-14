using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }
        public IActionResult ContatoAcao()
        {
            try 
            {
                Contato contato = new Contato();
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var ListaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid=Validator.TryValidateObject(contato, contexto, ListaMensagens,true);
                
                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);
                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var Texto in ListaMensagens)
                    {
                        sb.Append(Texto.ErrorMessage+"<br/>");
                    }
                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }
                

            }
            catch(Exception e)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro, tente novamento mais tarde!";
                //TODO - Emplementar Log
            }
            
            return View("Contato");
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult CadastroCliente()
        {
            return View();
        }
        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
