using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IImagemRepository
    {
        void CadastrarImagens(List<Imagem> ListaImagem);

        void Cadastrar(Imagem imagem);
        void Excluir(string caminho);
        void ExcluirImagensDoProduto(int produtoId);
    }
}
