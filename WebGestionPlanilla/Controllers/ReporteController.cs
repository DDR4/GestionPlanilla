using GP.Common;
using GP.Entities;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGestionPlanilla.Controllers
{
    public class ReporteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public void GenerarReporteTurno(string Periodo)
        {
            var NombreExcel = "Reporte Turno";

            // Recuperamos la data  de las consulta DB
            var bussingLogic = new GP.BusinessLogic.BLReporte();
            var data = bussingLogic.GetReporteTurno(Periodo);

            string[] Cabezeras = {
                    "Nombre y Apellido", "Turno", "Asistencia", "Tardanzas", "Faltas"
            };

            var wb = ReporteExcel.GenerarExcel(NombreExcel, Cabezeras, data.Data);


            var nameFile = NombreExcel +"_"+ DateTime.Now.ToString("dd_MM_yyyy HH:mm:ss") + ".xlsx";
            Response.AddHeader("content-disposition", "attachment; filename=" + nameFile);
            Response.ContentType = "application/octet-stream";
            Stream outStream = Response.OutputStream;
            wb.Write(outStream);
            outStream.Close();
            Response.End();
        }

        public void AddValue(IRow row, int cellnum, string value, ICellStyle styleBody, ISheet sheet)
        {
            ICell cell;
            cell = row.CreateCell(cellnum);
            cell.SetCellValue(value);
            cell.CellStyle = styleBody;
            sheet.AutoSizeColumn(cellnum);

        }
    }
}
