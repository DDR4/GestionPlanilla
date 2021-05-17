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
    public class DATurno
    {
        public IEnumerable<Turno> GetTurno(Turno obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Descripcion", obj.Descripcion);
                parm.Add("@Estado", obj.Estado);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "sp_Buscar_Turno",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Turno
                     {
                         Turno_Id = n.Single(d => d.Key.Equals("Turno_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("Turno_Descripcion")).Value.Parse<string>(),
                         Hora_Ingreso = n.Single(d => d.Key.Equals("Turno_Hora_Ingreso")).Value.Parse<string>(),
                         Hora_Salida = n.Single(d => d.Key.Equals("Turno_Hora_Salida")).Value.Parse<string>(),
                         Estado = n.Single(d => d.Key.Equals("Turno_Estado")).Value.Parse<int>(),
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

        public int InsertUpdateTurno(Turno obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Turno_Id", obj.Turno_Id);
                parm.Add("@Descripcion", obj.Descripcion);
                parm.Add("@Hora_Ingreso", obj.Hora_Ingreso);
                parm.Add("@Hora_Salida", obj.Hora_Salida);
                parm.Add("@Estado", obj.Estado);
                var result = connection.Execute(
                    sql: "sp_Insertar_Turno",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteTurno(Turno obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Turno_Id", obj.Turno_Id);
                var result = connection.Execute(
                    sql: "sp_Eliminar_Turno",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
