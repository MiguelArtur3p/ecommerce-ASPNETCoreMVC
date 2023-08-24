using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        //CRUD
        
        void Cadastrar(Produto produto);
        void Atualizar(Produto produto);
       
        void Excluir(int id);
        Produto ObterProduto(int id);
        IPagedList<Produto> ObterTodosProdutos(int? pagina,string pesquisa);
    }
}
