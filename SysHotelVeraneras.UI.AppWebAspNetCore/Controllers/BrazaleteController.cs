using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysHotelVeraneras.EntidadesDeNegocio;
using SysHotelVeraneras.LogicaDeNegocio;

namespace SysHotelVeraneras.UI.AppWebAspNetCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Administrador,Empleado")]

    public class BrazaleteController : Controller
    {
        BrazaleteBL BrazaleteBL = new BrazaleteBL();
        AsignacionBL AsignacionBL = new AsignacionBL();

        // GET: BrazaleteController
        public async Task<IActionResult> Index(Brazalete pBrazalete = null)
        {
            if (pBrazalete == null)
                pBrazalete = new Brazalete();
            if (pBrazalete.Top_Aux == 0)
                pBrazalete.Top_Aux = 10;
            else if (pBrazalete.Top_Aux == -1)
                pBrazalete.Top_Aux = 0;
            var taskBuscar = BrazaleteBL.BuscarIncluirAsignacionAsync(pBrazalete);
            var taskObtenerTodos = AsignacionBL.ObtenerTodosAsync();
            var Brazaletes = await taskBuscar;
            ViewBag.Asignacion = await taskObtenerTodos;
            ViewBag.Top = pBrazalete.Top_Aux;
            return View(Brazaletes);
        }

        // GET: ProductoController/Details/5
        public async Task<IActionResult> Details(int IdBrazalete)
        {
            var Brazalete = await BrazaleteBL.ObtenerPorIdAsync(new Brazalete { IdBrazalete = IdBrazalete });
            Brazalete.Asignacion = await AsignacionBL.ObtenerPorIdAsync(new Asignacion { IdAsignacion = Brazalete.IdAsignacion });
            return View(Brazalete);
        }

        // GET: ProductoController/Create

        public async Task<IActionResult> Create()
        {
            ViewBag.Asignacion = await AsignacionBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brazalete pBrazalete)
        {
            try
            {
                if (pBrazalete.Cantidad == null)
                {
                    pBrazalete.Cantidad = 0;
                }
                int result = await BrazaleteBL.CrearAsync(pBrazalete);
                return RedirectToAction(nameof(Index));
            
            }
            catch (Exception ex)
            {
                ViewBag.Asignacion = await AsignacionBL.ObtenerTodosAsync();
                ViewBag.Error = ex.Message;
                return View(pBrazalete);
            }
        }

        // GET: ProductoController/Edit/5
        public async Task<IActionResult> Edit(Brazalete pBrazalete)
        {
            var taskObtenerPorId = BrazaleteBL.ObtenerPorIdAsync(pBrazalete);
            var taskObtenerTodos = AsignacionBL.ObtenerTodosAsync();
            var Brazalete = await taskObtenerPorId;
            ViewBag.Asignacion = await taskObtenerTodos;
            ViewBag.Error = "";
            return View(Brazalete);
        }

        // POST: BrazaleteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdBrazalete, Brazalete pBrazalete)
        {
            try
            {
                if (pBrazalete.Cantidad == null)
                {
                    pBrazalete.Cantidad = 0;
                }
                int result = await BrazaleteBL.ModificarAsync(pBrazalete);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Asignacion = await AsignacionBL.ObtenerTodosAsync();
                return View(pBrazalete);
            }
        }

        // GET: BrazaleteController/Delete/5
        public async Task<IActionResult> Delete(Brazalete pBrazalete)
        {
            var Brazalete = await BrazaleteBL.ObtenerPorIdAsync(pBrazalete);
            Brazalete.Asignacion = await AsignacionBL.ObtenerPorIdAsync(new Asignacion { IdAsignacion = Brazalete.IdAsignacion });
            ViewBag.Error = "";
            return View(Brazalete);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdBrazalete, Brazalete pBrazalete)
        {
            try
            {
                int result = await BrazaleteBL.EliminarAsync(pBrazalete);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var Brazalete = await BrazaleteBL.ObtenerPorIdAsync(pBrazalete);
                if (Brazalete == null)
                    Brazalete = new Brazalete();
                if (Brazalete.IdBrazalete > 0)
                    Brazalete.Asignacion = await AsignacionBL.ObtenerPorIdAsync(new Asignacion { IdAsignacion = Brazalete.IdAsignacion });

                return View(Brazalete);
            }
        }
    }
}
