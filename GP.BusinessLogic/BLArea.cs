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
    public class BLArea
    {
        private DAArea repository;

        public BLArea()
        {
            repository = new DAArea();
        }

        public Response<IEnumerable<Area>> GetArea(Area obj)
        {
            try
            {
                var result = repository.GetArea(obj);
                return new Response<IEnumerable<Area>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Area>>(ex);
            }
        }

        public Response<int> InsertUpdateArea(Area obj)
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
