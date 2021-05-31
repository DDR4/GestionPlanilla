using GP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGestionPlanilla.Controllers
{
    public class VacacionesController : Controller
    {
        // GET: Vacaciones
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetVacaciones(Vacaciones obj)
        {
            var ctx = HttpContext.GetOwinContext();
            var tipoUsuario = ctx.Authentication.User.Claims.FirstOrDefault().Value;
            obj.Auditoria = new Auditoria
            {
                TipoUsuario = tipoUsuario
            };

            string draw = Request.Form.GetValues("draw")[0];
            int inicio = Convert.ToInt32(Request.Form.GetValues("start").FirstOrDefault());
            int fin = Convert.ToInt32(Request.Form.GetValues("length").FirstOrDefault());

            obj.Operacion = new Operacion
            {
                Inicio = (inicio / fin),
                Fin = fin
            };

            var bussingLogic = new GP.BusinessLogic.BLVacaciones();
            var response = bussingLogic.GetVacaciones(obj);

            var Datos = response.Data;
            int totalRecords = Datos.Any() ? Datos.FirstOrDefault().Operacion.TotalRows : 0;
            int recFilter = totalRecords;

            var result = (new
            {
                draw = Convert.ToInt32(draw),
                recordsTotal = totalRecords,
                recordsFiltered = recFilter,
                data = Datos
            });

            return Json(result);
        }

        public JsonResult GenerarVacacionesMasivo()
        {
            var bussingLogic = new GP.BusinessLogic.BLVacaciones();
            var response = bussingLogic.GenerarVacacionesMasivo();

            return Json(response);
        }

        public JsonResult DetalleVacaciones(Trabajador obj)
        {          
            var bussingLogic = new GP.BusinessLogic.BLVacaciones();
            var response = bussingLogic.DetalleVacaciones(obj);

            return Json(response);
        }

        public JsonResult InsertarDetalleVacaciones(DetalleVacaciones obj)
        {
            var bussingLogic = new GP.BusinessLogic.BLVacaciones();
            var response = bussingLogic.InsertarDetalleVacaciones(obj);

            return Json(response);
        }

    }
}
