using Microsoft.EntityFrameworkCore;
using SysHotelVeraneras.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.AccesoADatos
{
    public class InventarioDAL
    {
        public static async Task<int> CrearAsync(Inventario pInventario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                pInventario.FechaRegistro = DateTime.Now;
                bdContexto.Add(pInventario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModificarAsync(Inventario pInventario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var inventario = await bdContexto.Inventario.FirstOrDefaultAsync(s => s.IdInventario == pInventario.IdInventario);
                inventario.Movimiento = pInventario.Movimiento;
                inventario.Cantidad = pInventario.Cantidad;
                inventario.CorrelativoInicio = pInventario.CorrelativoFin;
                bdContexto.Update(inventario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> EliminarAsync(Inventario pInventario)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var inventario = await bdContexto.Inventario.FirstOrDefaultAsync(s => s.IdInventario == pInventario.IdInventario);
                bdContexto.Inventario.Remove(inventario);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Inventario> ObtenerPorIdAsync(Inventario pInventario)
        {
            var inventario = new Inventario();
            using (var bdContexto = new BDContexto())
            {
                inventario = await bdContexto.Inventario.FirstOrDefaultAsync(s => s.IdInventario == pInventario.IdInventario);
            }
            return inventario;
        }
        public static async Task<List<Inventario>> ObtenerTodosAsync()
        {
            var Inventarios = new List<Inventario>();
            using (var bdContexto = new BDContexto())
            {
                Inventarios = await bdContexto.Inventario.ToListAsync();
            }
            return Inventarios;
        }
        internal static IQueryable<Inventario> QuerySelect(IQueryable<Inventario> pQuery, Inventario pInventario)
        {
            if (pInventario.IdInventario > 0)
                pQuery = pQuery.Where(s => s.IdInventario == pInventario.IdInventario);
            if (pInventario.Cantidad > 0)
                pQuery = pQuery.Where(s => s.Cantidad == pInventario.Cantidad);
            if (pInventario.Movimiento > 0)
                pQuery = pQuery.Where(s => s.Movimiento == pInventario.Movimiento);
            if (pInventario.IdBrazalete > 0)
                pQuery = pQuery.Where(s => s.IdBrazalete == pInventario.IdBrazalete);
            pQuery = pQuery.OrderByDescending(s => s.IdInventario).AsQueryable();
            if (pInventario.Top_Aux > 0)
                pQuery = pQuery.Take(pInventario.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Inventario>> BuscarAsync(Inventario pInventario)
        {
            var Inventarios = new List<Inventario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Inventario.AsQueryable();
                select = QuerySelect(select, pInventario);
                Inventarios = await select.ToListAsync();
            }
            return Inventarios;
        }

        public static async Task<List<Inventario>> BuscarIncluirBrazaleteYUsuarioAsync(Inventario pInventario)
        {
            var Inventarios = new List<Inventario>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Inventario.AsQueryable();
                select = QuerySelect(select, pInventario).Include(a => a.Usuario).Include(a => a.Brazalete).AsQueryable();
                Inventarios = await select.ToListAsync();
            }
            return Inventarios;
        }
    }
}
