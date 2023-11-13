using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Consultorio_Seguros.Models
{
    public class Seguro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(50)]
        public string Codigo { get; set;}

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(150)]
        public string Nombre { get; set; }

        [RegularExpression("^(([1-9]{1}|[\\d]{2,})(\\.[\\d]+)?)$", ErrorMessage = "Solo se permiten numeros.")]        
        //[Precision(18,2)]
        public string SemiAsegurada { get; set; }
        
        [RegularExpression("^(([1-9]{1}|[\\d]{2,})(\\.[\\d]+)?)$", ErrorMessage = "Solo se permiten numeros.")]
        //[Precision(18, 2)]
        public string Prima { get; set; }
    }
}
