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
    public class BLCombos
    {
        private DACombos repository;

        public BLCombos()
        {
            repository = new DACombos();
        }

        public Response<IEnumerable<Area>> GetArea()
        {
            try
            {
                var result = repository.GetArea();
                return new Response<IEnumerable<Area>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Area>>(ex);
            }
        }

        public Response<IEnumerable<Turno>> GetTurno()
        {
            try
            {
                var result = repository.GetTurno();
                return new Response<IEnumerable<Turno>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Turno>>(ex);
            }
        }

        public Response<IEnumerable<Cargo>> GetCargo()
        {
            try
            {
                var result = repository.GetCargo();
                return new Response<IEnumerable<Cargo>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Cargo>>(ex);
            }
        }

        public Response<IEnumerable<TipoDocumento>> GetTipoDocumento()
        {
            try
            {
                var result = repository.GetTipoDocumento();
                return new Response<IEnumerable<TipoDocumento>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<TipoDocumento>>(ex);
            }
        }



    }
}
