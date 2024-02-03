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
    public class AsignacionController : Controller
    {
        AsignacionBL AsignacionBL = new AsignacionBL();
        // GET: AsignacionController
        public async Task<IActionResult> Index(Asignacion pAsignacion = null)
        {
            if (pAsignacion == null)
                pAsignacion = new Asignacion();
            if (pAsignacion.Top_Aux == 0)
                pAsignacion.Top_Aux = 10;
            else if (pAsignacion.Top_Aux == -1)
                pAsignacion.Top_Aux = 0;
            var Asignaciones = await AsignacionBL.BuscarAsync(pAsignacion);
            ViewBag.Top = pAsignacion.Top_Aux;
            return View(Asignaciones);
        }

        // GET: AsignacionContAsignacionler/Details/5
        public async Task<IActionResult> Details(int IdAsignacion)
        {
            var Asignacion = await AsignacionBL.ObtenerPorIdAsync(new Asignacion { IdAsignacion = IdAsignacion });
            return View(Asignacion);
        }

        // GET: AsignacionContAsignacionler/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: AsignacionContAsignacionler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asignacion pAsignacion)
        {
            try
            {
                int result = await AsignacionBL.CrearAsync(pAsignacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pAsignacion);
            }
        }

        // GET: AsignacionContAsignacionler/Edit/5
        public async Task<IActionResult> Edit(Asignacion pAsignacion)
        {
            var Asignacion = await AsignacionBL.ObtenerPorIdAsync(pAsignacion);
            ViewBag.Error = "";
            return View(Asignacion);
        }

        // POST: AsignacionContAsignacionler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdAsinacion, Asignacion pAsignacion)
        {
            try
            {
                int result = await AsignacionBL.ModificarAsync(pAsignacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pAsignacion);
            }
        }

        // GET: AsignacionContAsignacionler/Delete/5
        public async Task<IActionResult> Delete(Asignacion pAsignacion)
        {
            ViewBag.Error = "";
            var Asignacion = await AsignacionBL.ObtenerPorIdAsync(pAsignacion);
            return View(Asignacion);
        }

        // POST: AsignacionContAsignacionler/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int IdAsignacion, Asignacion pAsignacion)
        {
            try
            {
                int result = await AsignacionBL.EliminarAsync(pAsignacion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pAsignacion);
            }
        }
    }
}
