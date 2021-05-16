using GP.Entities;
using System.Web.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Linq;
using System;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebGestionPlanilla.Controllers
{
    public class HorasTrabajadasController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetHorasTrabajadas(Trabajador obj)
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

            var bussingLogic = new GP.BusinessLogic.BLHorasTrabajadas();

            if (!User.Identity.GetUserId().Equals("1"))
            {          
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                obj.Trabajador_Id = Convert.ToInt32(identity.Claims.Where(c => c.Type == "Trabajador_Id").Select(c => c.Value).SingleOrDefault());
            }                    

            var response = bussingLogic.GetHorasTrabajadas(obj);

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

    }
}