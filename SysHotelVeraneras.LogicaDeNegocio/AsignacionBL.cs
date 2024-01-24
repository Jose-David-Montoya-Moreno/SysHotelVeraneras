using SysHotelVeraneras.AccesoADatos;
using SysHotelVeraneras.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.LogicaDeNegocio
{
    public class AsignacionBL
    {
        public async Task<int> CrearAsync(Asignacion pAsignacion)
        {
            return await AsignacionDAL.CrearAsync(pAsignacion);
        }
        public async Task<int> ModificarAsync(Asignacion pAsignacion)
        {
            return await AsignacionDAL.ModificarAsync(pAsignacion);
        }
        public async Task<int> EliminarAsync(Asignacion pAsignacion)
        {
            return await AsignacionDAL.EliminarAsync(pAsignacion);
        }
        public async Task<Asignacion> ObtenerPorIdAsync(Asignacion pAsignacion)
        {
            return await AsignacionDAL.ObtenerPorIdAsync(pAsignacion);
        }
        public async Task<List<Asignacion>> ObtenerTodosAsync()
        {
            return await AsignacionDAL.ObtenerTodosAsync();
        }
        public async Task<List<Asignacion>> BuscarAsync(Asignacion pAsignacion)
        {
            return await AsignacionDAL.BuscarAsync(pAsignacion);
        }
    }
}
