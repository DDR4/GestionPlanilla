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
            var bussingLogic = new GP.BusinessLogic.BLCombos();
            var response = bussingLogic.GetArea();

            return Json(response);
        }

        public JsonResult GetTurno()
        {
            var bussingLogic = new GP.BusinessLogic.BLCombos();
            var response = bussingLogic.GetTurno();

            return Json(response);
        }

        public JsonResult GetCargo()
        {
            var bussingLogic = new GP.BusinessLogic.BLCombos();
            var response = bussingLogic.GetCargo();

            return Json(response);
        }

        public JsonResult GetTipoDocumento()
        {
            var bussingLogic = new GP.BusinessLogic.BLCombos();
            var response = bussingLogic.GetTipoDocumento();

            return Json(response);
        }
        public JsonResult GetTipoCalculoBoleta()
        {
            var bussingLogic = new GP.BusinessLogic.BLCombos();
            var response = bussingLogic.GetTipoCalculoBoleta();

            return Json(response);
        }

        public JsonResult GetTipoAFP()
        {
            var bussingLogic = new GP.BusinessLogic.BLCombos();
            var response = bussingLogic.GetTipoAFP();

            return Json(response);
        }

    }
}