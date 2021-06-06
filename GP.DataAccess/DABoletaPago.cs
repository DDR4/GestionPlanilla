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
    public class DABoletaPago
    {
        public IEnumerable<BoletaPago> GetBoletaPago(BoletaPago obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Periodo", obj.HorasTrabajadas.Periodo);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "sp_Buscar_Boleta",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new BoletaPago
                     {
                         Trabajador = new Trabajador
                         {
                             Trabajador_Id = n.Single(d => d.Key.Equals("Trabajador_Id")).Value.Parse<int>(),
                             Nombres = n.Single(d => d.Key.Equals("NombreApellido")).Value.Parse<string>(),
                             Area = new Area
                             {
                                 Descripcion = n.Single(d => d.Key.Equals("Area_Descripcion")).Value.Parse<string>()

                             },
                             Turno = new Turno
                             {
                                 Descripcion = n.Single(d => d.Key.Equals("Turno_Descripcion")).Value.Parse<string>()
                             },
                             Cargo = new Cargo
                             {
                                Descripcion = n.Single(d => d.Key.Equals("Cargo_Descripcion")).Value.Parse<string>()
                             }
                         },
                         HorasTrabajadas = new HorasTrabajadas
                         {
                             Periodo = n.Single(d => d.Key.Equals("Periodo")).Value.Parse<string>()
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
        public IEnumerable<BoletaPago> GetTrabajadores(BoletaPago obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Periodo", obj.HorasTrabajadas.Periodo);
                var result = connection.Query(
                    sql: "sp_Buscar_Trabajador_Periodo",
                    param: parm,
                    commandType: CommandType.StoredProcedure)
                    .Select(m => m as IDictionary<string, object>)
                    .Select(n => new BoletaPago
                    {
                        Indicador = n.Single(d => d.Key.Equals("Indicador")).Value.Parse<int>(),
                        Trabajador = new Trabajador
                        {
                            Trabajador_Id = n.Single(d => d.Key.Equals("Trabajador_Id")).Value.Parse<int>()
                        },
                      
                    });

                return result;
            }
        }


    }
}
