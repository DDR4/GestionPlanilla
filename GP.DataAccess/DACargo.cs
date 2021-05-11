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

namespace GestionPlanilla.DataAccess
{
    public class DACargo
    {
        public IEnumerable<Cargo> GetCargo(Cargo obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Descripcion", obj.Descripcion);
                parm.Add("@Area", obj.Area.Area_Id);
                parm.Add("@Estado", obj.Estado);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "sp_Buscar_Cargo",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Cargo
                     {
                         Cargo_Id = n.Single(d => d.Key.Equals("Cargo_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("Cargo_Descripcion")).Value.Parse<string>(),
                         Estado = n.Single(d => d.Key.Equals("Cargo_Estado")).Value.Parse<int>(),
                         Area = new Area
                         {
                             Area_Id = n.Single(d => d.Key.Equals("Area")).Value.Parse<int>(),
                             Descripcion = n.Single(d => d.Key.Equals("Area_Descripcion")).Value.Parse<string>(),
                         },
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

        public int InsertUpdateCargo(Cargo obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cargo_Id", obj.Cargo_Id);
                parm.Add("@Descripcion", obj.Descripcion);
                parm.Add("@Area", obj.Area.Area_Id);
                parm.Add("@Estado", obj.Estado);
                var result = connection.Execute(
                    sql: "sp_Insertar_Cargo",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteCargo(Cargo obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Cargo_Id", obj.Cargo_Id);
                var result = connection.Execute(
                    sql: "sp_Eliminar_Cargo",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
