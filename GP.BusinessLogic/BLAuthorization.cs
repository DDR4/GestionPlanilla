using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP.Entities;

namespace GP.BusinessLogic
{
    public class BLAuthorization
    {
        private DataAccess.DAAuthorization repository;

        public BLAuthorization()
        {
            repository = new DataAccess.DAAuthorization();
        }
        public async Task<Common.Response<Trabajador>> Authorize(Trabajador credential)
        {
            try
            {
                var result = await repository.Authorize(credential);

                if (result == null)
                {
                    return new Common.Response<Trabajador>("Trabajador o password incorrectos.");
                }

                return new Common.Response<Trabajador>(result);
            }
            catch (Exception ex)
            {
                return new Common.Response<Trabajador>(ex);
            }
        }
    }
}
