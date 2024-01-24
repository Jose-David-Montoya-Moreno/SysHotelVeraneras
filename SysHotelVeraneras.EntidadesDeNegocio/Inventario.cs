using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.EntidadesDeNegocio
{
   public class Inventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInventario { get; set; }

        [Required(ErrorMessage = "Movimiento es obligatorio")]
        public byte Movimiento { get; set; }

        [Required(ErrorMessage = "Cantidad es obligatorio")]
        public int Cantidad { get; set; }

        public int NuevoSaldo { get; set; }

        [Required(ErrorMessage = "Correlativo inicial es obligatorio")]
        public int CorrelativoInicio { get; set; }

        [Required(ErrorMessage = "Correltivo final es obligatorio")]
        public int CorrelativoFin { get; set; }

        [Display(Name = "Fecha registro")]
        public DateTime FechaRegistro { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Brazalete")]
        [Required(ErrorMessage = "Brazalete es obligatorio")]
        [Display(Name = "Brazalete")]
        public int IdBrazalete { get; set; }

         public Brazalete Brazalete { get; set; }

        public Usuario Usuario { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
