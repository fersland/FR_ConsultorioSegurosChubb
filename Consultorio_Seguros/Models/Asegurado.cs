using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultorio_Seguros.Models
{
    public class Asegurado
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        [ForeignKey(nameof(Clientes))]
        public int ClienteId {  get; set; }
        public virtual Cliente Clientes {  get; set; }

        [Required]
        [ForeignKey(nameof(Seguros))]
        public int SeguroId {  get; set; }
        public virtual Seguro Seguros { get; set; }

    }
}
