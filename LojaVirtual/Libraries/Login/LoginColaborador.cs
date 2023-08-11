using LojaVirtual.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Login
{
    public class LoginColaborador
    {
        private string _key = "Login.Colaborador";
        private Sessao.Sessao _sessao;
        public LoginColaborador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Colaborador colaborador)
        {
            string clienteJSONString = JsonConvert.SerializeObject(colaborador);
            _sessao.Cadastrar(_key, clienteJSONString);
        }
        public Colaborador GetColaborador()
        {
            if (_sessao.Existe(_key))
            {
                string colaboradorJSONString = _sessao.Consultar(_key);

                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJSONString);
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
