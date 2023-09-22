using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Models;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace LojaVirtual.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private IProdutoRepository _produtoRepository;
        private IMapper _mapper;
        private CarrinhoCompra _carrinhoCompra;
        public CarrinhoCompraController(IMapper mapper,CarrinhoCompra carrinhoCompra,IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _carrinhoCompra = carrinhoCompra;
            _produtoRepository = produtoRepository;
        }
        public IActionResult Index()
        {
            List<ProdutoItem>produtoItemCarrinho=_carrinhoCompra.Consultar();
            List<ProdutoItem> produtoItemCompleto = new List<ProdutoItem>();
            foreach(var item in produtoItemCarrinho)
            {
                
                Produto produto=_produtoRepository.ObterProduto(item.Id);

                ProdutoItem produtoItem = _mapper.Map<ProdutoItem>(produto);
                produtoItem.QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho;
                produtoItemCompleto.Add(produtoItem);
            }

            return View(produtoItemCompleto);
        }
        // Item ID
        public IActionResult AdicionarItem(int id)
        {
            Produto produto = _produtoRepository.ObterProduto(id);
            if (produto == null)
            {
                
                return View("NaoExisteItem");
            }
            else
            {
               
                var item = new ProdutoItem() { Id = id, QuantidadeProdutoCarrinho = 1 };
                _carrinhoCompra.Cadastrar(item);
                return RedirectToAction(nameof(Index));

            }

        }

        public IActionResult AlterarQuantidade(int id, int quantidade)
        {
           var item =new ProdutoItem() { Id = id, QuantidadeProdutoCarrinho = quantidade };
            _carrinhoCompra.Atualizar(item);
            return RedirectToAction(nameof(Index));


        }
        public IActionResult RemoverItem(int id)
        {
            _carrinhoCompra.Remover(new ProdutoItem() { Id = id });
            return RedirectToAction(nameof(Index));


        }
    }
}
