using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ImagemRepository : IImagemRepository
    {

        private LojaVirtualContext _lojaVirtualContext;
        public ImagemRepository(LojaVirtualContext lojaVirtualContext)
        {
            _lojaVirtualContext = lojaVirtualContext;

        }
        public void CadastrarImagens(List<Imagem> ListaImagem)
        {
            if (ListaImagem != null && ListaImagem.Count > 0)
            {
                foreach (var imagem in ListaImagem)
                {

                    Cadastrar(imagem);
                }
            }
            _lojaVirtualContext.SaveChanges();
        }
        public void Cadastrar(Imagem imagem)
        {
            _lojaVirtualContext.Add(imagem);
            _lojaVirtualContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            Imagem imagem = _lojaVirtualContext.Imagens.Find(id);
            _lojaVirtualContext.Imagens.Remove(imagem);
            _lojaVirtualContext.SaveChanges();
        }

        public void ExcluirImagensDoProduto(int produtoId)
        {
            List<Imagem> imagens = _lojaVirtualContext.Imagens.Where(a => a.ProdutoId == produtoId).ToList();
            foreach (Imagem imagem in imagens)
            {
                _lojaVirtualContext.Imagens.Remove(imagem);

            }
            _lojaVirtualContext.SaveChanges();
        }
    }
}
