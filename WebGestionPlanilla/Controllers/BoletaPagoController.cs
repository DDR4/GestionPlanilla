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

        public FileStreamResult DescargarBoletaPago(BoletaPago obj)
        {
            var bussingLogic = new GP.BusinessLogic.BLBoletaPago();
            var response = bussingLogic.DescargarBoletaPago(obj);

            MemoryStream ms = new MemoryStream(response.Data.Arraybytes, 0, 0, true, true);
            Response.AddHeader("content-disposition", "attachment;UREfilename= " + response.Data.Nombrearchivo);
            Response.Buffer = true;
            Response.Clear();
            Response.SuppressFormsAuthenticationRedirect = true;
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
            return new FileStreamResult(Response.OutputStream, "application/pdf");
        }

    }
}
