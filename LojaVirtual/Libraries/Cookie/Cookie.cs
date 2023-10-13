
using LojaVirtual.Libraries.Seguranca;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Cookie
{
    public class Cookie
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;
        public Cookie(IHttpContextAccessor contexto,IConfiguration configuration)
        {
            _context = contexto;
            _configuration = configuration;
        }
        public void Cadastrar(string key, string valor)
        {
            CookieOptions Options = new CookieOptions();
            Options.Expires = DateTime.Now.AddDays(7);
            var ValorCrypt = StringCipher.Encrypt(valor, _configuration.GetValue<string>("KeyCrypt"));
            _context.HttpContext.Response.Cookies.Append(key, ValorCrypt, Options);

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
            var ValorCrypt=_context.HttpContext.Request.Cookies[key];
            var Valor=StringCipher.Decrypt(ValorCrypt, _configuration.GetValue<string>("KeyCrypt"));
            return Valor;
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
    
