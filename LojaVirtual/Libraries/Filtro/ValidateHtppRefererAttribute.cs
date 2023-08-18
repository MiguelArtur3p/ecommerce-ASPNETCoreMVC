using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Filtro
{
    public class ValidateHtppRefererAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Antes 
            string referer = context.HttpContext.Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(referer))
            {
                context.Result = new ContentResult() { Content = "Acessso negado!" };
            }
            else
            {
                Uri uri = new Uri(referer);
                string hostReferer =uri.Host;
                string hostServidor = context.HttpContext.Request.Host.Host;
                if(hostReferer != hostServidor)
                {
                    context.Result = new ContentResult() { Content = "Acessso negado!" };
                }
            }
            


            
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Executado apos passar pelo controllador
           
        }


    }
}
