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
    class BLAfp
    {
        private DAAfp repository;

        public BLAfp()
        {
            repository = new DAAfp();
        }

        public Response<IEnumerable<Afp>> GetAfp(Afp obj)
        {
            try
            {
                var result = repository.GetAfp(obj);
                return new Response<IEnumerable<Afp>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Afp>>(ex);
            }
        }

        public Response<int> InsertUpdateArea(Afp obj)
        {
            try
            {
                var result = repository.InsertUpdateArea(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteArea(Area obj)
        {
            try
            {
                var result = repository.DeleteArea(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }
    }
}
