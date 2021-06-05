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
    public class DATrabajador
    {
        public IEnumerable<Trabajador> GetTrabajador(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();

                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                parm.Add("@Nombres", obj.Nombres);
                parm.Add("@Estado", obj.Estado);
                parm.Add("@NumPagina", obj.Operacion.Inicio);
                parm.Add("@TamPagina", obj.Operacion.Fin);
                var result = connection.Query(
                     sql: "sp_Buscar_Trabajador",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Trabajador
                     {
                         Trabajador_Id = n.Single(d => d.Key.Equals("Trabajador_Id")).Value.Parse<int>(),
                         Usuario = n.Single(d => d.Key.Equals("Trabajador_Usuario")).Value.Parse<string>(),
                         Contraseña = "******",
                         TipoDocumento = n.Single(d => d.Key.Equals("Tipo_Documento")).Value.Parse<int>(),
                         NumeroDocumento = n.Single(d => d.Key.Equals("Numero_Documento")).Value.Parse<string>(),
                         Nombres = n.Single(d => d.Key.Equals("Trabajador_Nombres")).Value.Parse<string>(),
                         ApellidoPaterno = n.Single(d => d.Key.Equals("Trabajador_ApellidoPaterno")).Value.Parse<string>(),
                         ApellidoMaterno = n.Single(d => d.Key.Equals("Trabajador_ApellidoMaterno")).Value.Parse<string>(),
                         FechaIngreso = n.Single(d => d.Key.Equals("Trabajador_FechaIngreso")).Value.Parse<DateTime>(),
                         Correo = n.Single(d => d.Key.Equals("Trabajador_Correo")).Value.Parse<string>(),
                         Sexo = n.Single(d => d.Key.Equals("Trabajador_Sexo")).Value.Parse<string>(),
                         Tipo = n.Single(d => d.Key.Equals("Trabajador_Tipo")).Value.Parse<int>(),
                         Salud = n.Single(d => d.Key.Equals("Trabajador_Salud")).Value.Parse<int>(),
                         AFP = n.Single(d => d.Key.Equals("Trabajador_AFP")).Value.Parse<int>(),
                         Estado = n.Single(d => d.Key.Equals("Trabajador_Estado")).Value.Parse<int>(),
                         Area = new Area
                         {
                             Area_Id = n.Single(d => d.Key.Equals("Area_Id")).Value.Parse<int>(),
                             Descripcion = n.Single(d => d.Key.Equals("Area_Descripcion")).Value.Parse<string>(),
                         },
                         Turno = new Turno
                         {
                             Turno_Id = n.Single(d => d.Key.Equals("Turno_Id")).Value.Parse<int>(),
                             Descripcion = n.Single(d => d.Key.Equals("Turno_Descripcion")).Value.Parse<string>(),
                         },
                         Cargo = new Cargo
                         {
                             Cargo_Id = n.Single(d => d.Key.Equals("Cargo_Id")).Value.Parse<int>(),
                             Descripcion = n.Single(d => d.Key.Equals("Cargo_Descripcion")).Value.Parse<string>(),
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

        public int InsertUpdateTrabajador(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                parm.Add("@Nombres", obj.Nombres);
                parm.Add("@ApellidoPaterno", obj.ApellidoPaterno);
                parm.Add("@ApellidoMaterno", obj.ApellidoMaterno);
                parm.Add("@Tipo_Documento", obj.TipoDocumento);
                parm.Add("@Numero_Documento", obj.NumeroDocumento);
                parm.Add("@Sexo", obj.Sexo);
                parm.Add("@Area", obj.Area.Area_Id);
                parm.Add("@Cargo", obj.Cargo.Cargo_Id);
                parm.Add("@FechaIngreso", obj.FechaIngreso);
                parm.Add("@Correo", obj.Correo);
                parm.Add("@Turno", obj.Turno.Turno_Id);
                parm.Add("@Usuario", obj.Usuario);
                parm.Add("@Contraseña", obj.Contraseña);
                parm.Add("@Tipo", obj.Tipo);
                parm.Add("@Salud", obj.Salud);
                parm.Add("@AFP", obj.AFP);
                parm.Add("@Estado", obj.Estado);   
                var result = connection.Execute(
                    sql: "sp_Insertar_Trabajador",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int DeleteTrabajador(Trabajador obj)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();
                parm.Add("@Trabajador_Id", obj.Trabajador_Id);
                var result = connection.Execute(
                    sql: "sp_Eliminar_Trabajador",
                    param: parm,
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public Trabajador ObtenerTrabajador(int? codigo)
        {
            using (var connection = Factory.ConnectionFactory())
            {
                connection.Open();
                var parm = new DynamicParameters();

                parm.Add("@Trabajador_Id", codigo);
                var result = connection.Query(
                     sql: "sp_Obtener_Trabajador",
                     param: parm,
                     commandType: CommandType.StoredProcedure)
                     .Select(m => m as IDictionary<string, object>)
                     .Select(n => new Trabajador
                     {
                         Trabajador_Id = n.Single(d => d.Key.Equals("Trabajador_Id")).Value.Parse<int>(),
                         Usuario = n.Single(d => d.Key.Equals("Trabajador_Usuario")).Value.Parse<string>(),
                         Contraseña = "******",
                         TipoDocumento = n.Single(d => d.Key.Equals("Tipo_Documento")).Value.Parse<int>(),
                         NumeroDocumento = n.Single(d => d.Key.Equals("Numero_Documento")).Value.Parse<string>(),
                         Nombres = n.Single(d => d.Key.Equals("Trabajador_Nombres")).Value.Parse<string>(),
                         ApellidoPaterno = n.Single(d => d.Key.Equals("Trabajador_ApellidoPaterno")).Value.Parse<string>(),
                         ApellidoMaterno = n.Single(d => d.Key.Equals("Trabajador_ApellidoMaterno")).Value.Parse<string>(),
                         FechaIngreso = n.Single(d => d.Key.Equals("Trabajador_FechaIngreso")).Value.Parse<DateTime>(),
                         Correo = n.Single(d => d.Key.Equals("Trabajador_Correo")).Value.Parse<string>(),
                         Sexo = n.Single(d => d.Key.Equals("Trabajador_Sexo")).Value.Parse<string>(),
                         Tipo = n.Single(d => d.Key.Equals("Trabajador_Tipo")).Value.Parse<int>(),
                         Estado = n.Single(d => d.Key.Equals("Trabajador_Estado")).Value.Parse<int>(),
                         Area = new Area
                         {
                             Area_Id = n.Single(d => d.Key.Equals("Area_Id")).Value.Parse<int>(),
                             Descripcion = n.Single(d => d.Key.Equals("Area_Descripcion")).Value.Parse<string>(),
                         },
                         Turno = new Turno
                         {
                             Turno_Id = n.Single(d => d.Key.Equals("Turno_Id")).Value.Parse<int>(),
                             Descripcion = n.Single(d => d.Key.Equals("Turno_Descripcion")).Value.Parse<string>(),
                         },
                         Cargo = new Cargo
                         {
                             Cargo_Id = n.Single(d => d.Key.Equals("Cargo_Id")).Value.Parse<int>(),
                             Descripcion = n.Single(d => d.Key.Equals("Cargo_Descripcion")).Value.Parse<string>(),
                             Sueldo = n.Single(d => d.Key.Equals("Cargo_Sueldo")).Value.Parse<decimal>(),
                         }
                     });

                return result.FirstOrDefault();
            }
        }
    }
}
