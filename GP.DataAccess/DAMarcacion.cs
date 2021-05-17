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
    public class DAMarcacion
    {
        public Turno GetHorarios(int Trabajador_Id)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();

                parm.Add("@Trabajador_Id", Trabajador_Id);
                var result = connection.Query(
                     sql: "sp_Obtener_Horario",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Turno
                     {
                         Hora_Ingreso = n.Single(d => d.Key.Equals("Turno_Hora_Ingreso")).Value.Parse<string>(),
                         Hora_Salida = n.Single(d => d.Key.Equals("Turno_Hora_Salida")).Value.Parse<string>()                        
                     });

                return result.FirstOrDefault();
            }
        }

        public Turno GetMarcaHorarios(int Trabajador_Id)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();

                parm.Add("@Trabajador_Id", Trabajador_Id);
                var result = connection.Query(
                     sql: "sp_Obtener_Marcar_Horario",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Turno
                     {
                         Marcar_Hora_Ingreso = n.Single(d => d.Key.Equals("Marcacion_Hora_Ingreso")).Value.Parse<DateTime>(),
                         Marcar_Hora_Salida = n.Single(d => d.Key.Equals("Marcacion_Hora_Salida")).Value.Parse<DateTime>()
                     });

                return result.FirstOrDefault();
            }
        }

        public DateTime MarcarIngreso(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();

                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                parm.Add("@HoraIngreso", obj.Turno.Marcar_Hora_Ingreso);
                var result = connection.Execute(
                     sql: "sp_Marcar_Ingreso",
                     param: parm,
                     commandType: CommandType.StoredProcedure);

                return obj.Turno.Marcar_Hora_Ingreso;
            }
        }

        public DateTime MarcarSalida(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();

                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                parm.Add("@HoraSalida", obj.Turno.Marcar_Hora_Salida);
                var result = connection.Execute(
                     sql: "sp_Marcar_Salida",
                     param: parm,
                     commandType: CommandType.StoredProcedure);

                return obj.Turno.Marcar_Hora_Salida;
            }
        }

    }
}
