using GP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGestionPlanilla.Controllers
{
    public class BoletaPagoController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetBoletaPago(BoletaPago obj)
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

            var bussingLogic = new GP.BusinessLogic.BLBoletaPago();
            var response = bussingLogic.GetBoletaPago(obj);

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

        public JsonResult GetTrabajadores(BoletaPago obj)
        {
            var bussingLogic = new GP.BusinessLogic.BLBoletaPago();
            var response = bussingLogic.GetTrabajadores(obj);

            return Json(response);
        }

        public void DescargarBoletaPago(int TrabajadorId, string Periodo)
        {
            var bussingLogic = new GP.BusinessLogic.BLBoletaPago();
            var response = bussingLogic.DescargarBoletaPago(TrabajadorId, Periodo);

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename="+ response.Data.Nombrearchivo + ".pdf");
            Response.ContentType = "application/pdf";
            Response.ClearContent();
            Response.OutputStream.Write(response.Data.Arraybytes, 0, response.Data.Arraybytes.Length);
            Response.End();
        }

    }
}
