using GP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebGestionPlanilla.Controllers
{
    public class TrabajadorController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTrabajador(Trabajador obj)
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

            var bussingLogic = new GP.BusinessLogic.BLTrabajador();
            var response = bussingLogic.GetTrabajador(obj);

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

        public JsonResult InsertUpdateTrabajador(Trabajador obj)
        {
            var bussingLogic = new GP.BusinessLogic.BLTrabajador();
            var response = bussingLogic.InsertUpdateTrabajador(obj);

            return Json(response);
        }

        public JsonResult DeleteTrabajador(Trabajador obj)
        {
            var bussingLogic = new GP.BusinessLogic.BLTrabajador();
            var response = bussingLogic.DeleteTrabajador(obj);

            return Json(response);
        }

    }
}