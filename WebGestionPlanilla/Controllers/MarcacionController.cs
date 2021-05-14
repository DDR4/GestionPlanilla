using GestionPlanilla.Entities;
using System.Web.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Linq;
using System;
using System.Globalization;
using GestionPlanilla.Common;

namespace WebGestionPlanilla.Controllers
{
    public class MarcacionController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetHorarios()
        {
            var bussingLogic = new GestionPlanilla.BusinessLogic.BLMarcacion();
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int Trabajador_Id = Convert.ToInt32(identity.Claims.Where(c => c.Type == "Trabajador_Id").Select(c => c.Value).SingleOrDefault());

            var response = bussingLogic.GetHorarios(Trabajador_Id);

            return Json(response);
        }

         public JsonResult GetMarcaHorarios()
        {
            var bussingLogic = new GestionPlanilla.BusinessLogic.BLMarcacion();
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int Trabajador_Id = Convert.ToInt32(identity.Claims.Where(c => c.Type == "Trabajador_Id").Select(c => c.Value).SingleOrDefault());

            var response = bussingLogic.GetMarcaHorarios(Trabajador_Id);

            return Json(response);
        }

        public JsonResult MarcarIngreso()
        {
            Trabajador obj = new Trabajador();
            var bussingLogic = new GestionPlanilla.BusinessLogic.BLMarcacion();
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int Trabajador_Id = Convert.ToInt32(identity.Claims.Where(c => c.Type == "Trabajador_Id").Select(c => c.Value).SingleOrDefault());
            obj.Trabajador_Id = Trabajador_Id;
            obj.Turno = new Turno { Marcar_Hora_Ingreso = DateTime.Now };

            var response = bussingLogic.MarcarIngreso(obj);

            return Json(response);
        }

        public JsonResult MarcarSalida()
        {
            Trabajador obj = new Trabajador();
            var bussingLogic = new GestionPlanilla.BusinessLogic.BLMarcacion();
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            int Trabajador_Id = Convert.ToInt32(identity.Claims.Where(c => c.Type == "Trabajador_Id").Select(c => c.Value).SingleOrDefault());
            obj.Trabajador_Id = Trabajador_Id;
            obj.Turno = new Turno { Marcar_Hora_Salida = DateTime.Now };

            var response = bussingLogic.MarcarSalida(obj);

            return Json(response);
        }

    }
}