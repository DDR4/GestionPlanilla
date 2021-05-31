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
    public class DAVacaciones
    {
        public IEnumerable<Vacaciones> GetVacaciones(Vacaciones obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Nombres", obj.NombreApellido);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "sp_Buscar_Vacaciones",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Vacaciones
                     {
                         
                         Vacaciones_Id = n.Single(d => d.Key.Equals("Vacaciones_Id")).Value.Parse<int>(),
                         Trabajador = new Trabajador
                         {
                             Trabajador_Id = n.Single(d => d.Key.Equals("Trabajador_Id")).Value.Parse<int>(),
                         },
                         NombreApellido = n.Single(d => d.Key.Equals("NombreApellido")).Value.Parse<string>(),
                         DiasDisponibles = n.Single(d => d.Key.Equals("Vacaciones_Disponibles")).Value.Parse<int>(),
                         DiasTotales = n.Single(d => d.Key.Equals("Vacaciones_Totales")).Value.Parse<int>(),
                         Auditoria = new Auditoria
                         {
                             TipoUsuario = obj.Auditoria.TipoUsuario,
                         },
                         Operacion = new Operacion
                         {
                             TotalRows = n.Single(d => d.Key.Equals("TotalRows")).Value.Parse<int>(),
                         }
                     });

                return result;
            }
        }

        public int GenerarVacacionesMasivo()
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                var result = connection.Execute(
                    sql: "sp_Generar_Masivo_Vacaciones",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public IEnumerable<DetalleVacaciones> DetalleVacaciones(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                parm.Add("@Periodo", obj.HorasTrabajadas.Periodo);
                var result = connection.Query(
                     sql: "sp_Buscar_Detalle_Vacaciones",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new DetalleVacaciones
                     {
                         Vacaciones_Id = n.Single(d => d.Key.Equals("Vacaciones_Id")).Value.Parse<int>(),
                         FechaInicio = n.Single(d => d.Key.Equals("VacacionesDetalle_Inicio")).Value.Parse<DateTime>(),
                         FechaFin = n.Single(d => d.Key.Equals("VacacionesDetalle_Fin")).Value.Parse<DateTime>(),
                         DiasVacaciones = n.Single(d => d.Key.Equals("VacacionesDetalle_Dias")).Value.Parse<int>()
                     });

                return result;
            }
        }

        public int InsertarDetalleVacaciones(DetalleVacaciones obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                parm.Add("@FechaInicio", obj.FechaInicio);
                parm.Add("@FechaFin", obj.FechaFin);
                var result = connection.Execute(
                    sql: "sp_Crear_Vacaciones",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
