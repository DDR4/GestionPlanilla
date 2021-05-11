using GestionPlanilla.Entities;
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
    public class CombosController : Controller
    {

        public JsonResult GetArea()
        {
            var bussingLogic = new GestionPlanilla.BusinessLogic.BLCombos();
            var response = bussingLogic.GetArea();

            return Json(response);
        }

        public JsonResult GetTurno()
        {
            var bussingLogic = new GestionPlanilla.BusinessLogic.BLCombos();
            var response = bussingLogic.GetTurno();

            return Json(response);
        }

        public JsonResult GetCargo()
        {
            var bussingLogic = new GestionPlanilla.BusinessLogic.BLCombos();
            var response = bussingLogic.GetCargo();

            return Json(response);
        }

    }
}