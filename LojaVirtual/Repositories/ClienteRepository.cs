using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly LojaVirtualContext _lojaVirtualContext;
        private IConfiguration _conf;

        public ClienteRepository(LojaVirtualContext lojaVirtualContext,IConfiguration conf)
        {
            _lojaVirtualContext = lojaVirtualContext;
            _conf = conf;
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
            Cliente cliente=_lojaVirtualContext.Clientes.Where(m => m.Email == email && m.Senha == senha).FirstOrDefault();
            return cliente; 
        }

        public Cliente ObterCliente(int id)
        {
            return _lojaVirtualContext.Clientes.Find(id);
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina,string pesquisa)
        {
            int NumeroPagina = pagina ?? 1;
            var bancoCliente = _lojaVirtualContext.Clientes.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoCliente = bancoCliente.Where(a => a.Nome.Contains(pesquisa.Trim()) || a.Email.Contains(pesquisa.Trim()));
            }
           
            return bancoCliente.ToPagedList<Cliente>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina"));
        }

        
    }
}
