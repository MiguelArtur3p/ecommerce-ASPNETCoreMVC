using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
    
        //CRUD
        void Cadastrar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Excluir(int id);
        Categoria ObterCategoria(int id);
        IEnumerable<Categoria> ObterTodasCategoria();
       IPagedList<Categoria> ObterTodasCategoria(int? pagina);
    }
}
