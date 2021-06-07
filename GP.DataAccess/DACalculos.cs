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
    public class DACalculos
    {
        public IEnumerable<Calculos> GetCalculos( Calculos obj)
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
                     sql: "sp_Buscar_Calculos",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Calculos
                     {
                         Calculo_Boleta_Id = n.Single(d => d.Key.Equals("CalculoBoleta_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("CalculoBoleta_Descripcion")).Value.Parse<string>(),
                         Estado = n.Single(d => d.Key.Equals("CalculoBoleta_Estado")).Value.Parse<int>(),
                         Monto = n.Single(d => d.Key.Equals("CalculoBoleta_Monto")).Value.Parse<decimal>(),
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

        public int InsertUpdateCalculos(Calculos obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@CalculoBoleta_Id", obj.Calculo_Boleta_Id);
                parm.Add("@Tipo_Calculo_Boleta_Id", obj.Tipo_Calculo_Boleta);
                parm.Add("@Descripcion", obj.Descripcion);
                parm.Add("@Estado", obj.Estado);
                var result = connection.Execute(
                    sql: "sp_Insertar_Calculos",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteCalculos(Calculos obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@CalculoBoleta_Id", obj.Calculo_Boleta_Id);
                var result = connection.Execute(
                    sql: "sp_Eliminar_Calculos",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

    }
}
