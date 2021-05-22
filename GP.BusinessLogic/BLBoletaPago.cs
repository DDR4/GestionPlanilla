﻿using System;
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

                Font tituloFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
                Font subtituloFont = new Font(Font.FontFamily.HELVETICA, 15, Font.NORMAL, BaseColor.BLACK);
                Font subtituloFont2 = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
                Font standardFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
                Font standardFont2 = new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);

                // Escribimos el encabezamiento en el documento

                Paragraph rasonsocial = AddParagraph("ABC S.A.C.", Element.ALIGN_LEFT, tituloFont);
                doc.Add(new Paragraph(rasonsocial));

                Paragraph ruc = AddParagraph("RUC: 20000000000", Element.ALIGN_LEFT, tituloFont);
                doc.Add(new Paragraph(ruc));

                doc.Add(Chunk.NEWLINE);

                Paragraph subtitulo = AddParagraph("BOLETA DE PAGO", Element.ALIGN_CENTER, subtituloFont);
                doc.Add(new Paragraph(subtitulo));

                Paragraph rangofecha = AddParagraph("01/05/2021 - 31/05/2021", Element.ALIGN_CENTER, tituloFont);
                doc.Add(new Paragraph(rangofecha));

                doc.Add(new Paragraph("\n"));

                //doc.Add(Chunk.NEWLINE);

                //Primera Parte
                PdfPTable tblElementos1 = new PdfPTable(6);
                tblElementos1.WidthPercentage = 100;

                string[] arrayElementos = { "Cod.Empleado:", "Nombre:", "DNI:", "Cargo:", "Fecha Ingreso", "Fecha de Cese" , "Dias Trabajados" , "Dias No Trabajados",
                                            "Dias vacaciones", "Sueldo","Horas Trabajados","Horas No Trabajados"};

                string[] arrayDatos = { "1", "Enrique Velasquez", "726030770", "Programador", "01/05/2020", "" , "20" , "5","0", "1500.00","160","40"};

                for (int i = 0; i < arrayElementos.Length; i++)
                {
                    float top = 0;
                    if (i < 3)
                    {
                        top = 1;
                    }
                    else if (i >= 3)
                    {
                        top = 0;
                    }

                    PdfPCell pdfPCell = AddPdfPCell(arrayElementos[i].ToString(), standardFont, 0, 0, top, Element.ALIGN_LEFT);
                    tblElementos1.AddCell(pdfPCell);

                    PdfPCell pdfPCelldatos = AddPdfPCell(arrayDatos[i].ToString(), standardFont, 0, 0, top, Element.ALIGN_LEFT);
                    tblElementos1.AddCell(pdfPCelldatos);
                }             

                doc.Add(tblElementos1);

                doc.Add(new Paragraph("\n"));

                //Segunda Parte

                PdfPTable tblElementos2 = new PdfPTable(3);
                tblElementos2.WidthPercentage = 100;

                string[] arrayElementos2 = { "Ingresos", "Descuentos", "Aportes de Empleador"};

                for (int i = 0; i < arrayElementos2.Length; i++)
                {
                    PdfPCell pdfPCell = AddPdfPCell(arrayElementos2[i].ToString(), subtituloFont2, 1 , 1, 1, Element.ALIGN_CENTER);
                    tblElementos2.AddCell(pdfPCell);
                }

                doc.Add(tblElementos2);

                //Tercera Parte

                PdfPTable tblElementos3 = new PdfPTable(6);
                tblElementos3.WidthPercentage = 100;

                string[] arrayElementos3 = { "Remuneracion Basica:", "AFP Aporte:", "ESSALUD:", "Vacaciones:", "AFP Comision", "" , "" , "AFP Seguros","","", "Retencion 5ta Categoria", ""};

                string[] arrayDatos3 = { "1500.00", "200.00", "150.00", "0.00", "36.00", "", "", "17.00", "", "", "42.00", "" };

                for (int i = 0; i < arrayElementos3.Length; i++)
                {
                    PdfPCell pdfPCell = AddPdfPCell(arrayElementos3[i].ToString(), standardFont, 0, 0, 0, Element.ALIGN_LEFT);
                    tblElementos3.AddCell(pdfPCell);

                    PdfPCell pdfPCelldatos = AddPdfPCell(arrayDatos3[i].ToString(), standardFont, 0, 0, 0, Element.ALIGN_RIGHT);
                    tblElementos3.AddCell(pdfPCelldatos);
                }

                doc.Add(tblElementos3);

                doc.Add(new Paragraph("\n"));

                //Cuarta Parte
                PdfPTable tblElementos4 = new PdfPTable(6);
                tblElementos4.WidthPercentage = 100;

                string[] arrayElementos4 = { "Total Ingresos", "Total Descuentos", "Total Aportes", "Total Neto", "", "" };

                string[] arrayDatos4 = { "1500.00", "295.00", "150.00", "1205.00", "", ""};

                for (int i = 0; i < arrayElementos4.Length; i++)
                {
                    PdfPCell pdfPCell = AddPdfPCell(arrayElementos4[i].ToString(), standardFont2, 0, 0, 0, Element.ALIGN_LEFT);
                    tblElementos4.AddCell(pdfPCell);

                    PdfPCell pdfPCelldatos = AddPdfPCell(arrayDatos4[i].ToString(), standardFont, 0, 0, 0, Element.ALIGN_RIGHT);
                    tblElementos4.AddCell(pdfPCelldatos);
                }

                doc.Add(tblElementos4);

                doc.Add(new Paragraph("\n"));

                Paragraph vacaciones = AddParagraph("Vacaciones", Element.ALIGN_LEFT, standardFont2);
                doc.Add(new Paragraph(vacaciones));

                //Quinta Parte
                PdfPTable tblElementos5 = new PdfPTable(4);
                tblElementos5.WidthPercentage = 50;
                tblElementos5.HorizontalAlignment = Element.ALIGN_LEFT;

                string[] arrayElementos5 = { "Inicio", "Fin", "Dias", "Tipo"};

                for (int i = 0; i < arrayElementos5.Length; i++)
                {
                    PdfPCell pdfPCell = AddPdfPCell(arrayElementos5[i].ToString(), standardFont2, 1, 1, 1, Element.ALIGN_CENTER);
                    tblElementos5.AddCell(pdfPCell);
                }

                doc.Add(tblElementos5);

                PdfPTable tblElementos6 = new PdfPTable(4);
                tblElementos6.WidthPercentage = 50;
                tblElementos6.HorizontalAlignment = Element.ALIGN_LEFT;

                string[] arrayElementos6 = { "", "", "0", "" };

                for (int i = 0; i < arrayElementos6.Length; i++)
                {
                    PdfPCell pdfPCell = AddPdfPCell(arrayElementos6[i].ToString(), standardFont, 0, 0, 0, Element.ALIGN_CENTER);
                    tblElementos6.AddCell(pdfPCell);
                }

                doc.Add(tblElementos6);

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

        public Paragraph AddParagraph(string descripcion,int alineacion,Font font)
        {
            Paragraph paragraph = new Paragraph(descripcion);
            paragraph.Alignment = alineacion;
            paragraph.Font = font;
            return paragraph;
        }

        public PdfPCell AddPdfPCell(string descripcion, Font font, float width, float widthbottom, float widthtop, int alineacion)
        {
            PdfPCell pdfPCell = new PdfPCell(new Phrase(descripcion, font));
            pdfPCell.BorderWidth = width;
            pdfPCell.BorderWidthBottom = widthbottom;
            pdfPCell.BorderWidthTop = widthtop;
            pdfPCell.HorizontalAlignment = alineacion;
            return pdfPCell;
        }
    }
}