using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Spacer.Models
{
    public class Espaco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(50,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Tipo de Espaço")]
        public int TipoEspacoId { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        public int Capacidade { get; set; }
        
        [Display(Name = "Foto 01")]
        [DataType(DataType.Upload)]
        public string Foto01 { get; set; }

        [Display(Name = "Foto 02")]
        [DataType(DataType.Upload)]
        public string Foto02 { get; set; }

        [Display(Name = "Foto 03")]
        [DataType(DataType.Upload)]
        public string Foto03 { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        public decimal Valor { get; set; }

        public virtual TipoEspaco TipoEspaco { get; set; }
    }
}
