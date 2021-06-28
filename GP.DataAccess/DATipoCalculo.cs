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
    public class DATipoCalculo
    {
        public IEnumerable<TipoCalculoBoleta> GetTipoCalculos(TipoCalculoBoleta obj)
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
                     sql: "sp_Buscar_Tipo_Calculos",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new TipoCalculoBoleta
                     {

                         Tipo_Calculo_Boleta_Id = n.Single(d => d.Key.Equals("Tipo_Calculo_Boleta_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("Tipo_Calculo_Boleta_Descripcion")).Value.Parse<string>(),
                         Estado = n.Single(d => d.Key.Equals("Tipo_Calculo_Boleta_Estado")).Value.Parse<int>(),
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

        public int InsertUpdateTipoCalculos(TipoCalculoBoleta obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Tipo_Calculo_Boleta_Id", obj.Tipo_Calculo_Boleta_Id); ;
                parm.Add("@Descripcion", obj.Descripcion);
                parm.Add("@Estado", obj.Estado);
                var result = connection.Execute(
                    sql: "sp_Insertar_Tipo_Calculos",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteTipoCalculos(TipoCalculoBoleta obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Tipo_Calculo_Boleta_Id", obj.Tipo_Calculo_Boleta_Id);
                var result = connection.Execute(
                    sql: "sp_Eliminar_Tipo_Calculos",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

    }
}
