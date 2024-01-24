using Microsoft.EntityFrameworkCore;
using SysHotelVeraneras.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.AccesoADatos
{
    public class BDContexto : DbContext
    {
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Brazalete> Brazalete { get; set; }
        public DbSet<Asignacion> Asignacion { get; set; }
        public DbSet<Inventario> Inventario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            optionsBuilder.UseSqlServer(@"workstation id=BDHOTELVERANERAS.mssql.somee.com;packet size=4096;user id=Hotelitoveranera_SQLLogin_1;pwd=r7k76hkfi5;data source=BDHOTELVERANERAS.mssql.somee.com;persist security info=False;initial catalog=BDHOTELVERANERAS;Encrypt=False;TrustServerCertificate=False;");
        }

    }
}
