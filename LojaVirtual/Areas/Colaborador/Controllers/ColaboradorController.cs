using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Texto;
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
                //TODO - Gerar senha aleatoria,salvar senha nova,Enviar o e-mail

                colaborador.Tipo = "C";
                _colaboradorRepository.Cadastrar(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult GerarSenha(int id)
        {
           Models.Colaborador colaborador= _colaboradorRepository.ObterColaborador(id);
            colaborador.Senha= KeyGenerator.GetUniqueKey(8);
            _colaboradorRepository.Atualizar(colaborador);

        }
        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            Models.Colaborador colaborador=_colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador, int id)
        {
            if (ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }
    }
}
