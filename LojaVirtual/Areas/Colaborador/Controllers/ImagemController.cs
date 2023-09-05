using LojaVirtual.Libraries.Arquivo;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ImagemController : Controller
    {
        private IImagemRepository _imagemRepository;
        public ImagemController(IImagemRepository imagemRepository)
        {
            _imagemRepository = imagemRepository;
        }
        [HttpPost]
        public IActionResult Armazenar(IFormFile file)
        {
            var Caminho = GerenciadorArquivo.CadastrarImagemProduto(file);
            if (Caminho.Length > 0)
            {
                return Ok(new { caminho = Caminho });
            }
            else
            {
                return new StatusCodeResult(500);
            }
          
        }
        public IActionResult Deletar(string caminho)
        {
            //TODO - IMPEDIR INPUT QUANDO JA TIVER UMA IMG

            string Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));
            _imagemRepository.Excluir(caminho);
            if (GerenciadorArquivo.ExcluirImagemProduto(caminho))
            {
                
                return Ok();
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
