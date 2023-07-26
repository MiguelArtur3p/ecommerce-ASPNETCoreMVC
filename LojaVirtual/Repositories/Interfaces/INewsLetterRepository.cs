using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface INewsLetterRepository
    {
        void Cadastrar(NewsLetterEmail newsLetter);
        IEnumerable<NewsLetterEmail> ObterTodasNewsLetter();
    }
}
