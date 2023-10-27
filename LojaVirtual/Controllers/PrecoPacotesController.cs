using AutoMapper;
using LojaVirtual.Gerenciador.Frete;
using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoPacotesController : ControllerBase
    {
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public PrecoPacotesController(CarrinhoCompra carrinhoCompra, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _carrinhoCompra = carrinhoCompra;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [Route("{tipoEnvio}")]
        public async Task<ActionResult> CalcularFrete(string tipoFrete)
        {
            double valorFrete;
            List<ProdutoItem> produtoItemCarrinho = _carrinhoCompra.Consultar();
            List<ProdutoItem> produtoItemCompleto = new List<ProdutoItem>();
            foreach (var item in produtoItemCarrinho)
            {

                Produto produto = _produtoRepository.ObterProduto(item.Id);

                ProdutoItem produtoItem = _mapper.Map<ProdutoItem>(produto);
                produtoItem.QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho;
                produtoItemCompleto.Add(produtoItem);
            }

            double qtdePacotes =await Task.Run(()=>CalcularPacote.CalcularPacoteDeProtudos(produtoItemCompleto));

            if (tipoFrete == "S")
            {
                valorFrete = qtdePacotes * 35;
                return Ok(valorFrete);
            }
            else if (tipoFrete == "C")
            {
                valorFrete = qtdePacotes * 20;
                return Ok(valorFrete);
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
