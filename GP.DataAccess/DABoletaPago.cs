using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GP.Common;
using GP.Entities;

namespace GP.DataAccess
{
    public class DABoletaPago
    {
        public IEnumerable<BoletaPago> GetTrabajadores(BoletaPago obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Periodo", obj.HorasTrabajadas.Periodo);
                var result = connection.Query(
                    sql: "sp_Buscar_Trabajador_Periodo",
                    param: parm,
                    commandType: CommandType.StoredProcedure)
                    .Select(m => m as IDictionary<string, object>)
                    .Select(n => new BoletaPago
                    {
                        Trabajador = new Trabajador
                        {
                            Trabajador_Id = n.Single(d => d.Key.Equals("Trabajador_Id")).Value.Parse<int>()
                        }                       
                    });

                return result;
            }
        }


    }
}
