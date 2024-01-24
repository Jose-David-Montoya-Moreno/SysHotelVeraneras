using SysHotelVeraneras.AccesoADatos;
using SysHotelVeraneras.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHotelVeraneras.LogicaDeNegocio
{
    public class BrazaleteBL
    {
        public async Task<int> CrearAsync(Brazalete pBrazalete)
        {
            return await BrazaleteDAL.CrearAsync(pBrazalete);
        }
        public async Task<int> ModificarAsync(Brazalete pBrazalete)
        {
            return await BrazaleteDAL.ModificarAsync(pBrazalete);
        }
        public async Task<int> EliminarAsync(Brazalete pBrazalete)
        {
            return await BrazaleteDAL.EliminarAsync(pBrazalete);
        }
        public async Task<Brazalete> ObtenerPorIdAsync(Brazalete pBrazalete)
        {
            return await BrazaleteDAL.ObtenerPorIdAsync(pBrazalete);
        }
        public async Task<List<Brazalete>> ObtenerTodosAsync()
        {
            return await BrazaleteDAL.ObtenerTodosAsync();
        }
        public async Task<List<Brazalete>> BuscarAsync(Brazalete pBrazalete)
        {
            return await BrazaleteDAL.BuscarAsync(pBrazalete);

        }
        public async Task<List<Brazalete>> BuscarIncluirAsignacionAsync(Brazalete pBrazalete)
        {
            return await BrazaleteDAL.BuscarIncluirAsignacionAsync(pBrazalete);

        }
    }
}
