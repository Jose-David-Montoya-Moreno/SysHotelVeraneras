using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.EntidadesDeNegocio
{
    public class Asignacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAsignacion { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string? Nombre { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        public ICollection<Brazalete>? Brazalete { get; set; }
    }
}
