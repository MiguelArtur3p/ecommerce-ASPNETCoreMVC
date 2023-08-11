using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LojaVirtual.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        IConfiguration _conf;
        private LojaVirtualContext _lojaVirtualContext;
        public CategoriaRepository(LojaVirtualContext lojaVirtualContext,IConfiguration conf)
        {
            _lojaVirtualContext = lojaVirtualContext;
            _conf = conf;
        }
        public void Atualizar(Categoria categoria)
        {
            _lojaVirtualContext.Categorias.Update(categoria);
            _lojaVirtualContext.SaveChanges();
        }

        public void Cadastrar(Categoria categoria)
        {
            _lojaVirtualContext.Categorias.Add(categoria);
            _lojaVirtualContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            Categoria categoria = ObterCategoria(id);
            _lojaVirtualContext.Categorias.Remove(categoria);
            _lojaVirtualContext.SaveChanges();
        }

        public Categoria ObterCategoria(int id)
        {
            return _lojaVirtualContext.Categorias.Find(id);
        }

        public IPagedList<Categoria> ObterTodasCategoria(int? pagina)
        {
            int NumeroPagina = pagina ?? 1;
            return _lojaVirtualContext.Categorias.Include(a=>a.CategoriaPai).ToPagedList<Categoria>(NumeroPagina,_conf.GetValue<int>("RegistroPorPagina"));
        }

        public IEnumerable<Categoria> ObterTodasCategoria()
        {
            return _lojaVirtualContext.Categorias;
        }
    }
}
