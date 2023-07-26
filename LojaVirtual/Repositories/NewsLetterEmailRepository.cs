using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class NewsLetterEmailRepository : INewsLetterRepository
    {
        private LojaVirtualContext _lojaVirtualContext;
        public NewsLetterEmailRepository(LojaVirtualContext lojaVirtualContext)
        {
            _lojaVirtualContext = lojaVirtualContext;
        }
        public void Cadastrar(NewsLetterEmail newsLetter)
        {
            _lojaVirtualContext.NewsLetterEmails.Add(newsLetter);
            _lojaVirtualContext.SaveChanges();
        }

        public IEnumerable<NewsLetterEmail> ObterTodasNewsLetter()
        {
            return _lojaVirtualContext.NewsLetterEmails.ToList();
        }
    }
}
