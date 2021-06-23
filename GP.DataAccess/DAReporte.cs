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
    public class DAReporte
    {
        public IEnumerable<Trabajador> GetReporteTurno(string Periodo)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Periodo", Periodo);
                var result = connection.Query(
                     sql: "sp_Reporte_Turno",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Trabajador
                     {
                         Nombres = n.Single(d => d.Key.Equals("NombreApellido")).Value.Parse<string>(),
                         Turno = new Turno
                         {
                             Descripcion = n.Single(d => d.Key.Equals("Turno_Descripcion")).Value.Parse<string>(),
                         },
                         HorasTrabajadas = new HorasTrabajadas
                         {
                             DiasTrabajados = n.Single(d => d.Key.Equals("Asistencias")).Value.Parse<int>(),
                             DiasTardanzas = n.Single(d => d.Key.Equals("Tardanzas")).Value.Parse<int>(),
                             DiasNoTrabajados = n.Single(d => d.Key.Equals("Faltas")).Value.Parse<int>(),
                         }
                     });

                return result;
            }
        }
    }
}
