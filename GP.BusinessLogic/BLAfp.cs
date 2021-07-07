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
    public class BLAfp
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

        public Response<int> InsertUpdateAfp(Afp obj)
        {
            try
            {
                var result = repository.InsertUpdateAfp(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteAfp(Afp obj)
        {
            try
            {
                var result = repository.DeleteAfp(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }
    }
}
