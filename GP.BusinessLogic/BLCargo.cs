using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionPlanilla.Common;
using GestionPlanilla.DataAccess;
using GestionPlanilla.Entities;

namespace GestionPlanilla.BusinessLogic
{
    public class BLCargo
    {
        private DACargo repository;

        public BLCargo()
        {
            repository = new DACargo();
        }

        public Response<IEnumerable<Cargo>> GetCargo(Cargo obj)
        {
            try
            {
                var result = repository.GetCargo(obj);
                return new Response<IEnumerable<Cargo>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Cargo>>(ex);
            }
        }

        public Response<int> InsertUpdateCargo(Cargo obj)
        {
            try
            {
                var result = repository.InsertUpdateCargo(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteCargo(Cargo obj)
        {
            try
            {
                var result = repository.DeleteCargo(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

    }
}
