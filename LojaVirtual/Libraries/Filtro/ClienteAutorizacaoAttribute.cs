using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace LojaVirtual.Libraries.Filtro
{
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private LoginCliente _loginCliente;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
           _loginCliente=(LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));
            Models.Cliente cliente = _loginCliente.GetCliente();
            if (cliente == null)
            {
                context.Result = new ContentResult() { Content = "Acesso Negado!" };
            }
        }
    }
}
