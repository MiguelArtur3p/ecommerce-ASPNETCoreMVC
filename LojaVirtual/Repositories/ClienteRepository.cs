using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly LojaVirtualContext _lojaVirtualContext;
        public ClienteRepository(LojaVirtualContext lojaVirtualContext)
        {
            _lojaVirtualContext = lojaVirtualContext;
        }
        public void Atualizar(Cliente cliente)
        {
            _lojaVirtualContext.Clientes.Update(cliente);
            _lojaVirtualContext.SaveChanges();
        }

        public void Cadastrar(Cliente cliente)
        {
            _lojaVirtualContext.Clientes.Add(cliente);
            _lojaVirtualContext.SaveChanges();
        }

        public void Excluir(int id)
        {
           Cliente cliente= ObterCliente(id);
            _lojaVirtualContext.Clientes.Remove(cliente);
            _lojaVirtualContext.SaveChanges();
        }

        public Cliente Login(string email, string senha)
        {
            Cliente cliente=_lojaVirtualContext.Clientes.Where(m => m.Email == email && m.Senha == senha).First();
            return cliente; 
        }

        public Cliente ObterCliente(int id)
        {
            return _lojaVirtualContext.Clientes.Find(id);
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            return _lojaVirtualContext.Clientes.ToList();
        }
    }
}
