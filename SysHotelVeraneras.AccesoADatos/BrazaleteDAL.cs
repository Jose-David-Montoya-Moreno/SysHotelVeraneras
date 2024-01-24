using Microsoft.EntityFrameworkCore;
using SysHotelVeraneras.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.AccesoADatos
{
    public class BrazaleteDAL
    {
        public static async Task<int> CrearAsync(Brazalete pBrazalete)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pBrazalete);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Brazalete pBrazalete)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var brazalete = await bdContexto.Brazalete.FirstOrDefaultAsync(s => s.IdBrazalete == pBrazalete.IdBrazalete);
                brazalete.Color = pBrazalete.Color;
                brazalete.Cantidad = pBrazalete.Cantidad;
                brazalete.IdAsignacion = pBrazalete.IdAsignacion;
                bdContexto.Update(brazalete);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Brazalete pBrazalete)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var brazalete = await bdContexto.Brazalete.FirstOrDefaultAsync(s => s.IdBrazalete == pBrazalete.IdBrazalete);
                bdContexto.Brazalete.Remove(brazalete);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Brazalete> ObtenerPorIdAsync(Brazalete pBrazalete)
        {
            var brazalete = new Brazalete();
            using (var bdContexto = new BDContexto())
            {
                brazalete = await bdContexto.Brazalete.FirstOrDefaultAsync(s => s.IdBrazalete == pBrazalete.IdBrazalete);
            }
            return brazalete;
        }
        public static async Task<List<Brazalete>> ObtenerTodosAsync()
        {
            var Brazaletes = new List<Brazalete>();
            using (var bdContexto = new BDContexto())
            {
                Brazaletes = await bdContexto.Brazalete.ToListAsync();
            }
            return Brazaletes;
        }
        internal static IQueryable<Brazalete> QuerySelect(IQueryable<Brazalete> pQuery, Brazalete pBrazalete)
        {
            if (pBrazalete.IdBrazalete > 0)
                pQuery = pQuery.Where(s => s.IdBrazalete == pBrazalete.IdBrazalete);
            if (!string.IsNullOrWhiteSpace(pBrazalete.Color))
                pQuery = pQuery.Where(s => s.Color.Contains(pBrazalete.Color));
            pQuery = pQuery.OrderByDescending(s => s.IdAsignacion).AsQueryable();
            if (pBrazalete.Top_Aux > 0)
                pQuery = pQuery.Take(pBrazalete.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Brazalete>> BuscarAsync(Brazalete pBrazalete)
        {
            var Brazaletes = new List<Brazalete>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Brazalete.AsQueryable();
                select = QuerySelect(select, pBrazalete);
                Brazaletes = await select.ToListAsync();
            }
            return Brazaletes;
        }

        public static async Task<List<Brazalete>> BuscarIncluirAsignacionAsync(Brazalete pBrazalete)
        {
            var Brazaletes = new List<Brazalete>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Brazalete.AsQueryable();
                select = QuerySelect(select, pBrazalete).Include(a => a.Asignacion).AsQueryable();
                Brazaletes = await select.ToListAsync();
            }
            return Brazaletes;
        }
    }
}
