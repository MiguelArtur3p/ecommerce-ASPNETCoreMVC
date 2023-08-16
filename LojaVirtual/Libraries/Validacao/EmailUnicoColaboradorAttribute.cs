using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Validacao
{
    public class EmailUnicoColaboradorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           
            string email = (value as string).Trim();
            IColaboradorRepository _colaboradorRepository = (IColaboradorRepository)validationContext.GetService(typeof(IColaboradorRepository));
            List<Colaborador> colaborador = _colaboradorRepository.ObterColaboradorPorEmail(email);
            Colaborador objColaborador=(Colaborador)validationContext.ObjectInstance;
            if (colaborador.Count > 1)
            {
                return new ValidationResult("E-mail já existente!");
            }
            if(colaborador.Count==1 && objColaborador.Id != colaborador[0].Id)
            {
                return new ValidationResult("E-mail já existente!");
            }
            return ValidationResult.Success;
        }
    }
}
