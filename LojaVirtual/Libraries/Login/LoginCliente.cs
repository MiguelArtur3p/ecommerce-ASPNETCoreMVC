using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Sessao;
using LojaVirtual.Models;
using Newtonsoft.Json;
namespace LojaVirtual.Libraries.Login
{
    public class LoginCliente
    {
        private string _key = "Login.Cliente";
        private Sessao.Sessao _sessao;
        public LoginCliente(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente cliente)
        {
            string clienteJSONString=JsonConvert.SerializeObject(cliente);
            _sessao.Cadastrar(_key, clienteJSONString);
        }
        public Cliente GetCliente()
        {
            if (_sessao.Existe(_key))
            {
                string clienteJSONString = _sessao.Consultar(_key);

                return JsonConvert.DeserializeObject<Cliente>(clienteJSONString);
            }
            else
            {
                return null;
            }
        }
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
