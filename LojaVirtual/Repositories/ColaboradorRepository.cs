using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly LojaVirtualContext _lojaVirtualContext;
        private IConfiguration _conf;
        public ColaboradorRepository(LojaVirtualContext lojaVirtualContext,IConfiguration configuration)
        {
            _lojaVirtualContext = lojaVirtualContext;
            _conf = configuration;
        }
        public void Atualizar(Colaborador colaborador)
        {
            _lojaVirtualContext.Colaboradores.Update(colaborador);
            _lojaVirtualContext.Entry(colaborador).Property(a => a.Senha).IsModified = false;
            _lojaVirtualContext.SaveChanges();
        }

        public void AtualizarSenha(Colaborador colaborador)
        {
            _lojaVirtualContext.Colaboradores.Update(colaborador);
            _lojaVirtualContext.Entry(colaborador).Property(a => a.Nome).IsModified = false;
            _lojaVirtualContext.Entry(colaborador).Property(a => a.Email).IsModified = false;
            _lojaVirtualContext.Entry(colaborador).Property(a => a.Senha).IsModified = false;
            _lojaVirtualContext.SaveChanges();
        }

        public void Cadastrar(Colaborador colaborador)
        {
            _lojaVirtualContext.Colaboradores.Add(colaborador);
            _lojaVirtualContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            Colaborador colaborador = ObterColaborador(id);
            _lojaVirtualContext.Colaboradores.Remove(colaborador);
            _lojaVirtualContext.SaveChanges();
        }

        public Colaborador Login(string Email, string senha)
        {
            Colaborador colaborador = _lojaVirtualContext.Colaboradores.Where(m => m.Email == Email && m.Senha == senha).FirstOrDefault();
            return colaborador;
        }

        public Colaborador ObterColaborador(int Id)
        {
            return _lojaVirtualContext.Colaboradores.Find(Id);
        }

        public List<Colaborador> ObterColaboradorPorEmail(string email)
        {
           return _lojaVirtualContext.Colaboradores.Where(a => a.Email == email).AsNoTracking().ToList();
        }

        public IPagedList<Colaborador> ObterTodosColaboradores(int? pagina)
        {
            int NumeroPagina = pagina ?? 1;
            return _lojaVirtualContext.Colaboradores.Where(a=>a.Tipo !="G").ToPagedList<Colaborador>(NumeroPagina, _conf.GetValue<int>("RegistroPorPagina"));
        }
    }
}
