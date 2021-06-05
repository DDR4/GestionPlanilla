using GP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Entities
{
    public class Trabajador
    {
        public int? Trabajador_Id { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaCese { get; set; }
        public string Correo { get; set; }
        public string Sexo { get; set; }
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int Tipo { get; set; }
        public int Salud { get; set; }
        public int AFP { get; set; }
        public int Estado { get; set; }
        public Area Area { get; set; }
        public Turno Turno { get; set; }
        public Cargo Cargo { get; set; }
        public HorasTrabajadas HorasTrabajadas { get; set; }
        public Operacion Operacion { get; set; }
        public Auditoria Auditoria { get; set; }
    }
}
