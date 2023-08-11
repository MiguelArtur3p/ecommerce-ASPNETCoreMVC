using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LojaVirtual.Database;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Filtro;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private LoginCliente _loginCliente;
        private IClienteRepository _repositoryCliente;
        private INewsLetterRepository _repositoryNewsLetter;

        public HomeController(IClienteRepository repositoryCliente, INewsLetterRepository repositoryNewsLetter, LoginCliente loginCliente)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsLetter = repositoryNewsLetter;
            _loginCliente = loginCliente;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm] NewsLetterEmail newsLetter)
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsLetter.Cadastrar(newsLetter);
                TempData["MSG_5"] = "Parabéns! Agora você recebera todas as nossas promoções por email!";
                return RedirectToAction(nameof(Index));
            }
            else
            {

            }
            return View();
        }
        private void NewsLetterEmail()
        {
            throw new NotImplementedException();
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
                bool isValid = Validator.TryValidateObject(contato, contexto, ListaMensagens, true);

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
                        sb.Append(Texto.ErrorMessage + "<br/>");
                    }
                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }


            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro, tente novamento mais tarde!";
                //TODO - Emplementar Log
            }

            return View("Contato");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _repositoryCliente.Login(cliente.Email, cliente.Senha);
            if (clienteDB != null)
            {
                _loginCliente.Login(clienteDB);
                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";
                return View();
            }

        }
        [HttpGet]
        [ClienteAutorizacao]
        public IActionResult Painel()
        {

            return new ContentResult() {Content= "Painel" };

        }
        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroCliente([FromForm] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repositoryCliente.Cadastrar(cliente);

                TempData["MSG_S"] = "Cadastro realizado com sucesso!";
                //TODO - Implementar redirecionamentos diferentes
                return RedirectToAction(nameof(CadastroCliente));
            }
            return View();
        }
        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
