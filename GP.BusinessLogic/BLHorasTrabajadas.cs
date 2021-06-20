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

        public Response<int> CrearDescansoMedico(Trabajador obj)
        {
            try
            {
                var result = repository.CrearDescansoMedico(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }


    }
}
