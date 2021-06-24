using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
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

            // Creación del libro excel xlsx.
            var wb = new XSSFWorkbook();

            // Creación del la hoja y se especifica un nombre
            ISheet sheet = wb.CreateSheet(NombreExcel);

            // Contadores para filas y columnas.
            int rownum = 0;
            int cellnum = 0;

            // Creacion del estilo de la letra para las cabeceras.
            var fontCab = wb.CreateFont();
            fontCab.FontHeightInPoints = 12;
            fontCab.FontName = "Calibri";
            fontCab.Color = HSSFColor.White.Index;

            // Creacion del color del estilo.
            var colorCab = new XSSFColor(new byte[] { 7, 105, 173 });

            // Se crea el estilo y se agrega el font al estilo
            var styleCab = (XSSFCellStyle)wb.CreateCellStyle();
            styleCab.SetFont(fontCab);
            styleCab.FillForegroundXSSFColor = colorCab;
            styleCab.FillPattern = FillPattern.SolidForeground;

            // Se crea la primera fila para las cabceras.
            IRow row = sheet.CreateRow(rownum++);
            ICell cell;

            foreach (var item in Cabezeras)
            {
                // Se crea celdas y se agrega las cabeceras
                cell = row.CreateCell(cellnum++);
                cell.SetCellValue(item);
                cell.CellStyle = styleCab;

                sheet.AutoSizeColumn(cellnum);
            }

            // Creacion del estilo de la letra para la data.
            var fontBody = wb.CreateFont();
            fontBody.FontHeightInPoints = 12;
            fontBody.FontName = "Arial";

            var styleBody = (XSSFCellStyle)wb.CreateCellStyle();
            styleBody.SetFont(fontBody);


            // Impresión de la data
            foreach (var item in data.Data)
            {
                cellnum = 0;
                row = sheet.CreateRow(rownum++);

                sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Nombres.ToString(), styleBody, sheet);
                AddValue(row, cellnum++, item.Turno.Descripcion.ToString(), styleBody, sheet);
                AddValue(row, cellnum++, item.HorasTrabajadas.DiasTrabajados.ToString(), styleBody, sheet);
                AddValue(row, cellnum++, item.HorasTrabajadas.DiasTardanzas.ToString(), styleBody, sheet);
                AddValue(row, cellnum++, item.HorasTrabajadas.DiasNoTrabajados.ToString(), styleBody, sheet);
            }


            var nameFile = NombreExcel + "_" + DateTime.Now.ToString("dd_MM_yyyy HH:mm:ss") + ".xlsx";
            Response.AddHeader("content-disposition", "attachment; filename=" + nameFile);
            Response.ContentType = "application/octet-stream";
            Stream outStream = Response.OutputStream;
            wb.Write(outStream);
            outStream.Close();
            Response.End();
        }

        public void GenerarReporteArea(string Periodo)
        {
            var NombreExcel = "Reporte Turno";

            // Recuperamos la data  de las consulta DB
            var bussingLogic = new GP.BusinessLogic.BLReporte();
            var data = bussingLogic.GetReporteTurno(Periodo);

            string[] Cabezeras = {
                    "Nombre y Apellido", "Turno", "Asistencia", "Tardanzas", "Faltas"
            };

            // Creación del libro excel xlsx.
            var wb = new XSSFWorkbook();

            // Creación del la hoja y se especifica un nombre
            ISheet sheet = wb.CreateSheet(NombreExcel);

            // Contadores para filas y columnas.
            int rownum = 0;
            int cellnum = 0;

            // Creacion del estilo de la letra para las cabeceras.
            var fontCab = wb.CreateFont();
            fontCab.FontHeightInPoints = 12;
            fontCab.FontName = "Calibri";
            fontCab.Color = HSSFColor.White.Index;

            // Creacion del color del estilo.
            var colorCab = new XSSFColor(new byte[] { 7, 105, 173 });

            // Se crea el estilo y se agrega el font al estilo
            var styleCab = (XSSFCellStyle)wb.CreateCellStyle();
            styleCab.SetFont(fontCab);
            styleCab.FillForegroundXSSFColor = colorCab;
            styleCab.FillPattern = FillPattern.SolidForeground;

            // Se crea la primera fila para las cabceras.
            IRow row = sheet.CreateRow(rownum++);
            ICell cell;

            foreach (var item in Cabezeras)
            {
                // Se crea celdas y se agrega las cabeceras
                cell = row.CreateCell(cellnum++);
                cell.SetCellValue(item);
                cell.CellStyle = styleCab;

                sheet.AutoSizeColumn(cellnum);
            }

            // Creacion del estilo de la letra para la data.
            var fontBody = wb.CreateFont();
            fontBody.FontHeightInPoints = 12;
            fontBody.FontName = "Arial";

            var styleBody = (XSSFCellStyle)wb.CreateCellStyle();
            styleBody.SetFont(fontBody);


            // Impresión de la data
            foreach (var item in data.Data)
            {
                cellnum = 0;
                row = sheet.CreateRow(rownum++);

                sheet.AutoSizeColumn(cellnum);
                AddValue(row, cellnum++, item.Nombres.ToString(), styleBody, sheet);
                AddValue(row, cellnum++, item.Turno.Descripcion.ToString(), styleBody, sheet);
                AddValue(row, cellnum++, item.HorasTrabajadas.DiasTrabajados.ToString(), styleBody, sheet);
                AddValue(row, cellnum++, item.HorasTrabajadas.DiasTardanzas.ToString(), styleBody, sheet);
                AddValue(row, cellnum++, item.HorasTrabajadas.DiasNoTrabajados.ToString(), styleBody, sheet);
            }


            var nameFile = NombreExcel + "_" + DateTime.Now.ToString("dd_MM_yyyy HH:mm:ss") + ".xlsx";
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
