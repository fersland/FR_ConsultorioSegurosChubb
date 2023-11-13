using System.ComponentModel.DataAnnotations;

namespace Consultorio_Seguros.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(10, ErrorMessage = "La Longitud de este campo es de 10 caracteres", MinimumLength = 10)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(40, ErrorMessage = "La Longitud permitida es de 2 y 40 caracteres", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Solo se permiten letras.")]
        public string Nombre { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(10, ErrorMessage = "La Longitud de este campo es de 10 caracteres", MinimumLength = 10)]
        public string Telefono { get; set; }
        
        [Range(18,80, ErrorMessage = "La edad permitida debe ser de 18 a 80 años")]
        //[StringLength(3, ErrorMessage = "La Longitud de este campo es de 3 caracteres", MinimumLength = 1)]
        public int Edad { get; set; }
    }
}
