using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GP.Common;
using GP.DataAccess;
using GP.Entities;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace GP.BusinessLogic
{
    public class BLBoletaPago
    {
        private DABoletaPago repository;

        public BLBoletaPago()
        {
            repository = new DABoletaPago();
        }

        public Response<IEnumerable<BoletaPago>> GetTrabajadores(BoletaPago obj)
        {
            try
            {
                var result = repository.GetTrabajadores(obj);

                string periodo = obj.HorasTrabajadas.Periodo;

                byte[] arraybytes = CrearBoletaPago(periodo);

                string body = CargarPlantilla(periodo);

                string nombrearchivo = "Boleta de Pago " + periodo;

                int i = 1; int? trabajadorid = 0; int contador = result.Count();
                while (contador >= i)
                {
                    trabajadorid = result.Where(x => x.Indicador == i).Select(y => y.Trabajador.Trabajador_Id).FirstOrDefault();
                    if (trabajadorid > 0)
                    {
                        EnvioCorreo.Send("gabrielx4air@gmail.com", nombrearchivo, body, arraybytes, nombrearchivo+".pdf");
                    }
                    i++;
                }

                return new Response<IEnumerable<BoletaPago>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<BoletaPago>>(ex);
            }
        }

        public byte[] CrearBoletaPago(string periodo)
        {
            Document doc = new Document(PageSize.LETTER);
            byte[] arraybytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();
                //pdfDoc = CrearBoletaPago(periodo);
                string titulo = "Boleta de Pago " + periodo;

                doc.AddTitle(titulo);
                doc.AddCreator("Enrique Velasquez");

                // Abrimos el archivo
                doc.Open();

                Font standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);

                // Escribimos el encabezamiento en el documento
                doc.Add(new Paragraph(titulo));
                doc.Add(Chunk.NEWLINE);

                // Creamos una tabla que contendrá el nombre, apellido y país
                // de nuestros visitante.
                PdfPTable tblPrueba = new PdfPTable(3);
                tblPrueba.WidthPercentage = 100;

                // Configuramos el título de las columnas de la tabla
                PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", standardFont));
                clNombre.BorderWidth = 1;
                clNombre.BorderWidthBottom = 0.75f;

                PdfPCell clApellido = new PdfPCell(new Phrase("Apellido", standardFont));
                clApellido.BorderWidth = 1;
                clApellido.BorderWidthBottom = 0.75f;

                PdfPCell clPais = new PdfPCell(new Phrase("País", standardFont));
                clPais.BorderWidth = 1;
                clPais.BorderWidthBottom = 0.75f;

                // Añadimos las celdas a la tabla
                tblPrueba.AddCell(clNombre);
                tblPrueba.AddCell(clApellido);
                tblPrueba.AddCell(clPais);

                // Llenamos la tabla con información
                clNombre = new PdfPCell(new Phrase("Roberto", standardFont));
                clNombre.BorderWidth = 1;

                clApellido = new PdfPCell(new Phrase("Torres", standardFont));
                clApellido.BorderWidth = 1;

                clPais = new PdfPCell(new Phrase("Puerto Rico", standardFont));
                clPais.BorderWidth = 1;

                // Añadimos las celdas a la tabla
                tblPrueba.AddCell(clNombre);
                tblPrueba.AddCell(clApellido);
                tblPrueba.AddCell(clPais);

                doc.Add(tblPrueba);

                doc.Close();
                arraybytes = memoryStream.ToArray();
                memoryStream.Close();
            }

            return arraybytes;

        }


        public string CargarPlantilla(string periodo)
        {
            var urlpantilla = HttpContext.Current.Request.PhysicalApplicationPath + @"Views\Plantillas\PlantillaCorreo.cshtml";

            string body = File.ReadAllText(urlpantilla);

            body = body.Replace("{{periodo}}", periodo);

            return body;
        }

    }
}
