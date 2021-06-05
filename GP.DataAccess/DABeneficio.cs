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
    public class DABeneficio
    {
        public Beneficio GetSeguro(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Trabajador_Id", obj.Trabajador_Id);;
                var result = connection.Query(
                     sql: "sp_Calculos_Trabajador",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Beneficio
                     {
                         EsSalud = n.Single(d => d.Key.Equals("EsSalud")).Value.Parse<decimal>(),
                         AFPAporte = n.Single(d => d.Key.Equals("MontoAFP")).Value.Parse<decimal>(),
                         AFPComision = n.Single(d => d.Key.Equals("MontoAFPComision")).Value.Parse<decimal>(),
                         AFPSeguro = n.Single(d => d.Key.Equals("MontoAFPSeguro")).Value.Parse<decimal>()
                     });

                return result.FirstOrDefault();
            }
        }
    }
}
