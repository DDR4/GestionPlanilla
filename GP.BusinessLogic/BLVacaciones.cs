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
    public class BLVacaciones
    {
        private DAVacaciones repository;

        public BLVacaciones()
        {
            repository = new DAVacaciones();
        }

        public Response<IEnumerable<Vacaciones>> GetVacaciones(Vacaciones obj)
        {
            try
            {
                var result = repository.GetVacaciones(obj);
                return new Response<IEnumerable<Vacaciones>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Vacaciones>>(ex);
            }
        }

        public Response<int> GenerarVacacionesMasivo()
        {
            try
            {
                var result = repository.GenerarVacacionesMasivo();
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<IEnumerable<DetalleVacaciones>> DetalleVacaciones(Trabajador obj)
        {
            try
            {
                var result = repository.DetalleVacaciones(obj);
                return new Response<IEnumerable<DetalleVacaciones>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<DetalleVacaciones>>(ex);
            }
        }

        public Response<int> InsertarDetalleVacaciones(DetalleVacaciones obj)
        {
            try
            {
                var result = repository.InsertarDetalleVacaciones(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

    }
}
