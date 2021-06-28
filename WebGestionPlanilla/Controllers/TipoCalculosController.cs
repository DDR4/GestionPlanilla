using GP.Entities; //LLAMAR PARA la entidad
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGestionPlanilla.Controllers
{
    public class TipoCalculosController : Controller
    {
        // GET: Calculos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Calculos/Details/5
        public JsonResult GetTipoCalculos(TipoCalculoBoleta obj)
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

            var bussingLogic = new GP.BusinessLogic.BLTipoCalculo();
            var response = bussingLogic.GetTipoCalculos(obj);

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
        public JsonResult InsertUpdateTipoCalculos(TipoCalculoBoleta obj)
        {
            var bussingLogic = new GP.BusinessLogic.BLTipoCalculo();
            var response = bussingLogic.InsertUpdateTipoCalculos(obj);

            return Json(response);
        }

        public JsonResult DeleteTipoCalculos(TipoCalculoBoleta obj)
        {
            var bussingLogic = new GP.BusinessLogic.BLTipoCalculo();
            var response = bussingLogic.DeleteTipoCalculos(obj);

            return Json(response);
        }

    }
}
