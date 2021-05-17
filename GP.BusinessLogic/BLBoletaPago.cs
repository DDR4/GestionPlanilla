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
                return new Response<IEnumerable<BoletaPago>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<BoletaPago>>(ex);
            }
        }       

    }
}
