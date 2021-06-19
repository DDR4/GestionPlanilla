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
    public class DAHorasTrabajadas
    {
        public IEnumerable<Trabajador> GetHorasTrabajadas(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();

                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                parm.Add("@Periodo", obj.HorasTrabajadas.Periodo);
                parm.Add("@NombreApellido", obj.Nombres);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "sp_Obtener_HorasTrabajadas",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Trabajador
                     {
                         Nombres = n.Single(d => d.Key.Equals("NombreApellido")).Value.Parse<string>(),
                         HorasTrabajadas = new HorasTrabajadas
                         {
                             Horas_Trabajadas = n.Single(d => d.Key.Equals("Horas_Trabajadas")).Value.Parse<int>(),
                             Horas_Tardanzas = n.Single(d => d.Key.Equals("Horas_Trabajadas_Tardanzas")).Value.Parse<int>(),
                             Periodo = n.Single(d => d.Key.Equals("Horas_Trabajadas_Periodo")).Value.Parse<string>(),
                             Tipo = n.Single(d => d.Key.Equals("Horas_Trabajadas_Tipo")).Value.Parse<int>(),
                         },                     
                         Operacion = new Operacion
                         {
                             TotalRows = n.Single(d => d.Key.Equals("TotalRows")).Value.Parse<int>(),
                         }
                     });

                return result;
            }
        }

        public HorasTrabajadas CalculaHorasTrabajadas(string periodo, Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Periodo", periodo);
                parm.Add("@TrabajadorId", obj.Trabajador_Id);
                var result = connection.Query(
                     sql: "sp_Calcular_Horas_Periodo",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new HorasTrabajadas
                     {
                        PrimerDia = n.Single(d => d.Key.Equals("PrimerDia")).Value.Parse<DateTime>(),
                        UltimoDia = n.Single(d => d.Key.Equals("UltimoDia")).Value.Parse<DateTime>(),
                        DiasTrabajados = n.Single(d => d.Key.Equals("DiasTrabajados")).Value.Parse<int>(),
                        Horas_Trabajadas = n.Single(d => d.Key.Equals("HorasTrabajadas")).Value.Parse<int>(),
                        DiasNoTrabajados = n.Single(d => d.Key.Equals("DiasNoTrabajados")).Value.Parse<int>(),
                        HorasNoTrabajados = n.Single(d => d.Key.Equals("HorasNoTrabajadas")).Value.Parse<int>(),    
                     });

                return result.FirstOrDefault();
            }
        }
    }
}
