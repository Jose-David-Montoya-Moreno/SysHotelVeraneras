using Microsoft.EntityFrameworkCore;
using SysHotelVeraneras.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.AccesoADatos
{
    public class AsignacionDAL
    {
        public static async Task<int> CrearAsync(Asignacion pAsignacion)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pAsignacion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Asignacion pAsignacion)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var asignacion = await bdContexto.Asignacion.FirstOrDefaultAsync(s => s.IdAsignacion == pAsignacion.IdAsignacion);
                asignacion.Nombre = pAsignacion.Nombre;
                bdContexto.Update(asignacion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Asignacion pAsignacion)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var asignacion = await bdContexto.Asignacion.FirstOrDefaultAsync(s => s.IdAsignacion == pAsignacion.IdAsignacion);
                bdContexto.Asignacion.Remove(asignacion);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Asignacion> ObtenerPorIdAsync(Asignacion pAsignacion)
        {
            var asignacion = new Asignacion();
            using (var bdContexto = new BDContexto())
            {
                asignacion = await bdContexto.Asignacion.FirstOrDefaultAsync(s => s.IdAsignacion == pAsignacion.IdAsignacion);
            }
            return asignacion;
        }
        public static async Task<List<Asignacion>> ObtenerTodosAsync()
        {
            var asignaciones = new List<Asignacion>();
            using (var bdContexto = new BDContexto())
            {
                asignaciones = await bdContexto.Asignacion.ToListAsync();
            }
            return asignaciones;
        }
        internal static IQueryable<Asignacion> QuerySelect(IQueryable<Asignacion> pQuery, Asignacion pAsignacion)
        {
            if (pAsignacion.IdAsignacion > 0)
                pQuery = pQuery.Where(s => s.IdAsignacion == pAsignacion.IdAsignacion);
            if (!string.IsNullOrWhiteSpace(pAsignacion.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pAsignacion.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.IdAsignacion).AsQueryable();
            if (pAsignacion.Top_Aux > 0)
                pQuery = pQuery.Take(pAsignacion.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Asignacion>> BuscarAsync(Asignacion pAsignacion)
        {
            var asignaciones = new List<Asignacion>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Asignacion.AsQueryable();
                select = QuerySelect(select, pAsignacion);
                asignaciones = await select.ToListAsync();
            }
            return asignaciones;
        }
    }
}
