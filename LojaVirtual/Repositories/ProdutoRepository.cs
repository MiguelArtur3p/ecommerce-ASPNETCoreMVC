using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        IConfiguration _conf;
        private LojaVirtualContext _lojaVirtualContext;
        public ProdutoRepository(LojaVirtualContext lojaVirtualContext, IConfiguration conf)
        {
            _lojaVirtualContext = lojaVirtualContext;
            _conf = conf;
        }
        public void Atualizar(Produto produto)
        {
            _lojaVirtualContext.Produtos.Update(produto);
            _lojaVirtualContext.SaveChanges();
        }

        public void Cadastrar(Produto produto)
        {
            _lojaVirtualContext.Produtos.Add(produto);
            _lojaVirtualContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            Produto produto = ObterProduto(id);
            _lojaVirtualContext.Produtos.Remove(produto);
            _lojaVirtualContext.SaveChanges();
        }

        public Produto ObterProduto(int id)
        {
           return _lojaVirtualContext.Produtos.Include(a=>a.Imagens).Where(a=>a.Id==id).FirstOrDefault();
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa)
        {
            int NumeroPagina = pagina ?? 1;
            var bancoProduto = _lojaVirtualContext.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoProduto = bancoProduto.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }

            return bancoProduto.Include(a => a.Imagens).ToPagedList<Produto>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina"));
        }
    }
}
