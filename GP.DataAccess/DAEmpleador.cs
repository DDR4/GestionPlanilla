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
    public class DAEmpleador
    {
        public Empleador ObtenerEmpleador()
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                var result = connection.Query(
                     sql: "sp_Obtener_Empleador",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Empleador
                     {
                         Empleador_Id = n.Single(d => d.Key.Equals("Empleador_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("Empleador_Descripcion")).Value.Parse<string>(),
                         Ruc = n.Single(d => d.Key.Equals("Empleador_Ruc")).Value.Parse<string>(),
                         Rubro = n.Single(d => d.Key.Equals("Empleador_Rubro")).Value.Parse<string>(),
                         Sede = n.Single(d => d.Key.Equals("Empleador_Sede")).Value.Parse<string>(),
                         Direccion = n.Single(d => d.Key.Equals("Empleador_Direcion")).Value.Parse<string>(),
                         Telefono = n.Single(d => d.Key.Equals("Empleador_Telefono")).Value.Parse<string>(),
                         Estado = n.Single(d => d.Key.Equals("Empleador_Estado")).Value.Parse<int>(),
                       
                     });

                return result.FirstOrDefault();
            }
        }

        public int InsertUpdateArea(Area obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Area_Id", obj.Area_Id);
                parm.Add("@Descripcion", obj.Descripcion);
                parm.Add("@Estado", obj.Estado);
                var result = connection.Execute(
                    sql: "sp_Insertar_Area",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteArea(Area obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Area_Id", obj.Area_Id);
                var result = connection.Execute(
                    sql: "sp_Eliminar_Area",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
