using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GP.Common;
using GP.DataAccess;
using GP.Entities;

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

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment("d:/CVEnriqueVelasquez.docx");

                var urlpantilla = HttpContext.Current.Request.PhysicalApplicationPath + @"Views\Plantillas\PlantillaCorreo.cshtml";

                string body = System.IO.File.ReadAllText(urlpantilla);

                body = body.Replace("{{periodo}}", periodo);

               

                int i = 1; int? trabajadorid = 0;
                while (result.Count() <= i)
                {
                    trabajadorid = result.Where(x => x.Indicador == i).Select(y => y.Trabajador.Trabajador_Id).FirstOrDefault();
                    if (trabajadorid > 0)
                    {
                        EnvioCorreo.Send("gabrielx4air@gmail.com", "Boleta de Pago" + periodo, body, attachment);
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

    }
}
