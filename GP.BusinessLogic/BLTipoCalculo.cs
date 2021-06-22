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
    public class BLTipoCalculo
    {
        private DATipoCalculo repository;

        public BLTipoCalculo()
        {
            repository = new DATipoCalculo();
        }

        public Response<IEnumerable<TipoCalculoBoleta>> GetTipoCalculos(TipoCalculoBoleta obj)
        {
            try
            {
                var result = repository.GetTipoCalculos(obj);
                return new Response<IEnumerable<TipoCalculoBoleta>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<TipoCalculoBoleta>>(ex);
            }
        }
        public Response<int> InsertUpdateTipoCalculos(TipoCalculoBoleta obj)
        {
            try
            {
                var result = repository.InsertUpdateTipoCalculos(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteCalculos(TipoCalculoBoleta obj)
        {
            try
            {
                var result = repository.DeleteTipoCalculos(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

    }
}
