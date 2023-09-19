using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.CarrinhoCompra
{
    public class CarrinhoCompra
    {
        private string Key = "Carrinho.Compras";
        private Cookie.Cookie _cookie;
        public CarrinhoCompra(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }
        /*
         * CRUD
         * Adicionar Item, Remover Item,Alterar Quantidade
         */
        public void Cadastrar(Item item)
        {
            if (_cookie.Existe(Key))
            {

            }
            else
            {

            }


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
        public List<Item> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                JsonConvert.DeserializeObject<List<Item>>(valor);
            }
            else
            {
                return new List<Item>();
            }
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
    public class Item
    {
        public int? id { get; set; }
        public int? Quantidade { get; set; }
    }
   
}
