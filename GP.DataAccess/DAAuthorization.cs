using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GestionPlanilla.Common;
using GestionPlanilla.Entities;

namespace GestionPlanilla.DataAccess
{
    public class DAAuthorization
    {
        public Task<Trabajador> Authorize(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();

                var parameter = new DynamicParameters();
                parameter.Add("@Usuario", obj.Usuario);
                parameter.Add("@Contraseña", obj.Contraseña);

                var result = connection.Query(
                    sql: "sp_Login",
                    param: parameter,
                    commandType: CommandType.StoredProcedure)
                    .Select(m => m as IDictionary<string, object>)
                    .Select(n => new Trabajador
                    {
                        Trabajador_Id = n.Single(d => d.Key.Equals("Trabajador_Id")).Value.Parse<int>(),
                        Nombres = n.Single(d => d.Key.Equals("Trabajador_Nombres")).Value.Parse<string>(),
                        Tipo = n.Single(d => d.Key.Equals("Trabajador_Tipo")).Value.Parse<int>(),
                        Area = new Area
                        {
                            Area_Id = n.Single(d => d.Key.Equals("Area")).Value.Parse<int>(),
                        },
                        Turno = new Turno
                        {
                            Turno_Id = n.Single(d => d.Key.Equals("Turno")).Value.Parse<int>(),
                        },
                        Cargo = new Cargo
                        {
                            Cargo_Id = n.Single(d => d.Key.Equals("Cargo")).Value.Parse<int>(),
                        }
                    });

                return Task.FromResult(result.FirstOrDefault());

            }


        }
    }
}
