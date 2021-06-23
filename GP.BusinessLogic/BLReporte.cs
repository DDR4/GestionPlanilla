using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP.Common;
using GP.DataAccess;
using GP.Entities;

namespace GP.BusinessLogic
{
    public class BLReporte
    {
        private DAReporte repository;

        public BLReporte()
        {
            repository = new DAReporte();
        }

        public Response<IEnumerable<Trabajador>> GetReporteTurno(string Periodo)
        {
            try
            {
                var result = repository.GetReporteTurno(Periodo);
                return new Response<IEnumerable<Trabajador>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Trabajador>>(ex);
            }
        }

    }
}
