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
    public class DACombos
    {
        public IEnumerable<Area> GetArea()
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                var result = connection.Query(
                     sql: "sp_Obtener_Area",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Area
                     {
                         Area_Id = n.Single(d => d.Key.Equals("Area_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("Area_Descripcion")).Value.Parse<string>()                        
                     });

                return result;
            }
        }

        public IEnumerable<Turno> GetTurno()
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                var result = connection.Query(
                     sql: "sp_Obtener_Turno",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Turno
                     {
                         Turno_Id = n.Single(d => d.Key.Equals("Turno_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("Turno_Descripcion")).Value.Parse<string>()
                     });

                return result;
            }
        }

        public IEnumerable<Cargo> GetCargo()
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                var result = connection.Query(
                     sql: "sp_Obtener_Cargo",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Cargo
                     {
                         Cargo_Id = n.Single(d => d.Key.Equals("Cargo_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("Cargo_Descripcion")).Value.Parse<string>()
                     });

                return result;
            }
        }

        public IEnumerable<TipoDocumento> GetTipoDocumento()
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                var result = connection.Query(
                     sql: "sp_Obtener_Tipo_Documento",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new TipoDocumento
                     {
                         TipoDocumento_Id = n.Single(d => d.Key.Equals("Tipo_Documento_Id")).Value.Parse<int>(),
                         Descripcion = n.Single(d => d.Key.Equals("Tipo_Documento_Descripcion")).Value.Parse<string>()
                     });

                return result;
            }
        }
    }
}
