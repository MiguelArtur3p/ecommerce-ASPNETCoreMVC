using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        //frete - correios
        public double Peso { get; set; }
        public int Largura { get; set; }
        public int Altura { get; set; }
        public int Comprimento { get; set; }

        //banco de dados -relacionamento
        public int CategoriaId { get; set; }//foreignkey
        // POO - associações entre obj
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }
        //EF - ORM- UNI OS 2
        public virtual ICollection<Imagem> Imagens { get; set; }
    }
}
