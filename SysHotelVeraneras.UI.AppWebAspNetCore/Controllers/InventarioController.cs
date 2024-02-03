using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysHotelVeraneras.AccesoADatos;
using SysHotelVeraneras.EntidadesDeNegocio;
using SysHotelVeraneras.LogicaDeNegocio;
using SysHotelVeraneras.UI.AppWebAspNetCore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SysHotelVeraneras.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Administrador,Empleado")]
    public class InventarioController : Controller
    {
        InventarioBL inventarioBL = new InventarioBL();
        BrazaleteBL BrazaleteBL = new BrazaleteBL();
        UsuarioBL usuarioBL = new UsuarioBL();

        // GET: DetallePedidoController
        public async Task<IActionResult> Index( DateTime fInicio, DateTime fFinal,Inventario pInventario = null)       
        {
            if (pInventario == null)
                pInventario = new Inventario();
            if (pInventario.Top_Aux == 0)
                pInventario.Top_Aux = 10;
            else if (pInventario.Top_Aux == -1)
                pInventario.Top_Aux = 0;
            var taskBuscar = inventarioBL.BuscarIncluirBrazaleteYUsuarioAsync(pInventario);
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosBrazalete = BrazaleteBL.ObtenerTodosAsync();
            var inventario = await taskBuscar;
            ViewBag.Top = pInventario.Top_Aux;
            ViewBag.Inventario = await taskBuscar;
            ViewBag.Usuario = await taskObtenerTodosUsuarios;
            ViewBag.Brazalete = await taskObtenerTodosBrazalete;
            if (fInicio.Year != 1 && fFinal.Year != 1)
            {
                ViewBag.Inventario = inventario.Where(r => r.FechaRegistro.Date >= fInicio.Date && r.FechaRegistro.Date <= fFinal.Date).ToList();
            }
            else
            {
                ViewBag.Inventario = inventario;
            }
            return View(inventario);
        }

        // GET: DetallePedidoController/Details/5
        public async Task<IActionResult> Details(int IdInventario)
        {
            var Inventario = await inventarioBL.ObtenerPorIdAsync(new Inventario { IdInventario = IdInventario });
            Inventario.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = Inventario.IdUsuario });
            Inventario.Brazalete = await BrazaleteBL.ObtenerPorIdAsync(new Brazalete { IdBrazalete = Inventario.IdBrazalete });

            return View(Inventario);
        }

        // GET: DetallePedidoController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
            ViewBag.Brazalete = await BrazaleteBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Inventario pInventario)
        {
            try
            {
                int result = await inventarioBL.CrearAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
                ViewBag.Brazalete = await BrazaleteBL.ObtenerTodosAsync();
                return View(pInventario);
            }
        }

        // GET: DetallePedidoController/Edit/5
        public async Task<IActionResult> Edit(Inventario pInventario)
        {
            var taskObtenerPorId = inventarioBL.ObtenerPorIdAsync(pInventario);
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosBrazalete = BrazaleteBL.ObtenerTodosAsync();
            var Inventario = await taskObtenerPorId;
            ViewBag.Usuario = await taskObtenerTodosUsuarios;
            ViewBag.Brazalete = await taskObtenerTodosBrazalete;
            ViewBag.Error = "";
            return View(Inventario);
        }

        // POST: DetallePedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdIventario, Inventario pInventario)
        {
            try
            {
                int result = await inventarioBL.ModificarAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
                ViewBag.Brazalete = await BrazaleteBL.ObtenerTodosAsync();
                return View(pInventario);
            }
        }

        // GET: DetallePedidoController/Delete/5
        public async Task<IActionResult> Delete(Inventario pInventario)
        {
            var Inventario = await inventarioBL.ObtenerPorIdAsync(pInventario);
            Inventario.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = Inventario.IdUsuario });
            Inventario.Brazalete = await BrazaleteBL.ObtenerPorIdAsync(new Brazalete { IdBrazalete = Inventario.IdBrazalete });
            ViewBag.Error = "";

            return View(Inventario);
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdInventario, Inventario pInventario)
        {
            try
            {
                int result = await inventarioBL.EliminarAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var Inventario = await inventarioBL.ObtenerPorIdAsync(pInventario);
                if (Inventario == null)
                    Inventario = new Inventario();
                if (Inventario.IdInventario > 0)
                    Inventario.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = Inventario.IdUsuario });
                if (Inventario.IdInventario > 0)
                    Inventario.Brazalete = await BrazaleteBL.ObtenerPorIdAsync(new Brazalete { IdBrazalete = Inventario.IdBrazalete });
                return View(Inventario);
            }
        }

        public async Task<IActionResult> Ajuste()
        {
            ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
            ViewBag.Brazalete = await BrazaleteBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetallePedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ajuste(Inventario pInventario, int Movimiento)
        {
            try
            {
                pInventario.IdUsuario = global.idu;

                Brazalete objBrazalete = new Brazalete();
                objBrazalete.IdBrazalete =  pInventario.IdBrazalete;
                objBrazalete = await BrazaleteBL.ObtenerPorIdAsync(objBrazalete);

                if (Movimiento == 1)
                {
                    objBrazalete.Cantidad = objBrazalete.Cantidad + pInventario.Cantidad;
                    await BrazaleteBL.ModificarAsync(objBrazalete);
                }
                else if (Movimiento == 2)
                {
                    objBrazalete.Cantidad = objBrazalete.Cantidad - pInventario.Cantidad;
                    await BrazaleteBL.ModificarAsync(objBrazalete);
                }
                pInventario.NuevoSaldo = objBrazalete.Cantidad  ;
                int result = await inventarioBL.CrearAsync(pInventario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
                ViewBag.Brazalete = await BrazaleteBL.ObtenerTodosAsync();
                return View(pInventario);
            }
        }
    }

}
