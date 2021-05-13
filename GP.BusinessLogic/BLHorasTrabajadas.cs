using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionPlanilla.Common;
using GestionPlanilla.DataAccess;
using GestionPlanilla.Entities;
using SL.Entities;

namespace GestionPlanilla.BusinessLogic
{
    public class BLHorasTrabajadas
    {
        private DAHorasTrabajadas repository;

        public BLHorasTrabajadas()
        {
            repository = new DAHorasTrabajadas();
        }

        public Response<IEnumerable<Trabajador>> GetHorasTrabajadas(Trabajador obj)
        {
            try
            {
                var result = repository.GetHorasTrabajadas(obj);
                return new Response<IEnumerable<Trabajador>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Trabajador>>(ex);
            }
        }       
        

    }
}
