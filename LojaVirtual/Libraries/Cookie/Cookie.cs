using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Cookie
{
    public class Cookie
    {
        private IHttpContextAccessor _context;
        public Cookie(IHttpContextAccessor contexto)
        {
            _context = contexto;
        }
        public void Cadastrar(string key, string valor)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(7);
            _context.HttpContext.Response.Cookies.Append(key, valor, Options);

        }
        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
            {
                Remover(key);
            }
            Cadastrar(key, valor);

        }
        public void Remover(string key)
        {
            _context.HttpContext.Response.Cookies.Delete(key);

        }
        public string Consultar(string key)
        {
            return _context.HttpContext.Request.Cookies[key];
        }
        public bool Existe(string key)
        {
            if (_context.HttpContext.Request.Cookies[key] == null)
            {
                return false;
            }

            return true;

        }
        public void RemoverTodos()
        {
            var listaCookie = _context.HttpContext.Request.Cookies.ToList();
            foreach (var cookie in listaCookie)
            {
                Remover(cookie.Key);
            }

        }
    }
}
    
