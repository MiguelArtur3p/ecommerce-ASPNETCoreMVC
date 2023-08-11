using LojaVirtual.Libraries.Lang;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        public ColaboradorController(IColaboradorRepository colaborador)
        {
            _colaboradorRepository = colaborador;
        }
        public IActionResult Index(int? pagina)
        {
            IPagedList<Models.Colaborador> colaboradores=_colaboradorRepository.ObterTodosColaboradores(pagina);
            return View(colaboradores);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm]Models.Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                colaborador.Tipo = "C";
                _colaboradorRepository.Cadastrar(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador, int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            return View();
        }
    }
}
