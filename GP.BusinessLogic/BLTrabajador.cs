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
    public class BLTrabajador
    {
        private DATrabajador repository;

        public BLTrabajador()
        {
            repository = new DATrabajador();
        }

        public Response<IEnumerable<Trabajador>> GetTrabajador(Trabajador obj)
        {
            try
            {
                var result = repository.GetTrabajador(obj);
                return new Response<IEnumerable<Trabajador>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Trabajador>>(ex);
            }
        }

        public Response<int> InsertUpdateTrabajador(Trabajador obj)
        {
            try
            {
                var result = repository.InsertUpdateTrabajador(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteTrabajador(Trabajador obj)
        {
            try
            {
                var result = repository.DeleteTrabajador(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

    }
}
