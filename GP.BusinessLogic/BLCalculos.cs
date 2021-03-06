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
    public class BLCalculos
    {
        private DACalculos repository;

        public BLCalculos()
        {
            repository = new DACalculos();
        }

        public Response<IEnumerable<Calculos>> GetCalculos(Calculos obj)
        {
            try
            {
                var result = repository.GetCalculos(obj);
                return new Response<IEnumerable<Calculos>>(result);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Calculos>>(ex);
            }
        }
        public Response<int> InsertUpdateCalculos(Calculos obj)
        {
            try
            {
                var result = repository.InsertUpdateCalculos(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

        public Response<int> DeleteCalculos(Calculos obj)
        {
            try
            {
                var result = repository.DeleteCalculos(obj);
                return new Response<int>(result);
            }
            catch (Exception ex)
            {
                return new Response<int>(ex);
            }
        }

    }
}
