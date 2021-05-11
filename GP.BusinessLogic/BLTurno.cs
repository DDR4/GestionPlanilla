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
    public class BLTurno
    {
        private DATurno repository;

        public BLTurno()
        {
            repository = new DATurno();
        }

        public Response<IEnumerable<Turno>> GetTurno(Turno obj)
        {
            try
            {
                var result = repository.GetTurno(obj);
                return new Response<IEnumerable<Turno>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Turno>>(ex);
            }
        }

        public Response<int> InsertUpdateTurno(Turno obj)
        {
            try
            {
                var result = repository.InsertUpdateTurno(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteTurno(Turno obj)
        {
            try
            {
                var result = repository.DeleteTurno(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

    }
}
