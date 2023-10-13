using LojaVirtual.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class MenuViewComponent : ViewComponent
    {

        private ICategoriaRepository _categoriaRepository;
        public MenuViewComponent(ICategoriaRepository categoriaRepostiroy)
        {
            _categoriaRepository = categoriaRepostiroy;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ListaCategoria = await Task.Run(() => _categoriaRepository.ObterTodasCategoria().ToList());
            return View(ListaCategoria);
        }
        //public  IViewComponentResult Invoke()
        //{
        //    var ListaCategoria = _categoriaRepository.ObterTodasCategoria().ToList();
        //    return View(ListaCategoria);
        //}
    }
}
