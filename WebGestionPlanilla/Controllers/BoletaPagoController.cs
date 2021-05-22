using GP.Entities;
using System;
using System.Collections.Generic;
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

        public JsonResult GetTrabajadores(BoletaPago obj)
        {
            var bussingLogic = new GP.BusinessLogic.BLBoletaPago();
            var response = bussingLogic.GetTrabajadores(obj);

            return Json(response);
        }

    }
}
