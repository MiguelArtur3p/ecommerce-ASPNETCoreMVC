using LojaVirtual.Gerenciador.Frete;
using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Models.ProdutoAgregador;
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
        public PrecoPacotesController(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }
        
        [Route("{tipoEnvio}")]        
        public async Task<ActionResult> CalcularFrete(string tipoEnvio)
        {
            double valorFrete = 0.0;
            ////List<ProdutoItem> produtoItemCarrinho =_carrinhoCompra.Consultar();
            ////double qtdePacotes=CalcularPacote.CalcularPacoteDeProtudos(produtoItemCarrinho);
            //int qtdePacotes = 2;
            //if (tipoEnvio == "S")
            //{
            //    valorFrete = qtdePacotes * 35;
            //    return Ok(valorFrete);
            //}
            //else if (tipoEnvio == "C")
            //{
            //    valorFrete = qtdePacotes * 20;
            //    return Ok(valorFrete);
            //}
            //else
            //{
            //    return BadRequest();
            //}
            return Ok();
        }
       
    }
}
