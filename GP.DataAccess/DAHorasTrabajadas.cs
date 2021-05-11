using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GestionPlanilla.Common;
using GestionPlanilla.DataAccess;
using GestionPlanilla.Entities;
using SL.Entities;

namespace GestionPlanilla.DataAccess
{
    public class DAHorasTrabajadas
    {
        public IEnumerable<HorasTrabajadas> GetHorasTrabajadas(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();

                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                parm.Add("@Periodo", obj.HorasTrabajadas.Periodo);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "sp_Obtener_HorasTrabajadas",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new HorasTrabajadas
                     {
                         Horas_Trabajadas = n.Single(d => d.Key.Equals("Horas_Trabajadas")).Value.Parse<int>(),
                         Horas_Tardanzas = n.Single(d => d.Key.Equals("Horas_Trabajadas_Tardanzas")).Value.Parse<int>(),
                         Faltas = n.Single(d => d.Key.Equals("Horas_Trabajadas_Faltas")).Value.Parse<int>(),
                         Periodo = n.Single(d => d.Key.Equals("Horas_Trabajadas_Periodo")).Value.Parse<string>(),
                         Operacion = new Operacion
                         {
                             TotalRows = n.Single(d => d.Key.Equals("TotalRows")).Value.Parse<int>(),
                         }
                     });

                return result;
            }
        }
    }
}
