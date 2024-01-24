using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.EntidadesDeNegocio
{
    public class Brazalete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBrazalete { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string? Color { get; set; }
        [Required(ErrorMessage = "Color es obligatorio")]
        public int Cantidad { get; set; }
        
        [ForeignKey("Asignacion")]
        [Required(ErrorMessage = "Asignacion es obligatorio")]
        [Display(Name = "Asignacion")]
        public int IdAsignacion { get; set; }

        public Asignacion Asignacion { get; set; }

        public ICollection<Inventario>? Inventario { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
